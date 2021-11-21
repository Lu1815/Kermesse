using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kermesse.Models;
using Microsoft.Reporting.WebForms;

namespace Kermesse.Controllers
{
    public class DenominacionsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: Denominacions
        [Authorize]
        public ActionResult Index(string dato)
        {
            var denominacion = from m in db.Denominacions select m;

            if (!string.IsNullOrEmpty(dato))
            {
                denominacion = denominacion.Where(m => m.valor.ToString().Contains(dato) || m.valorLetras.Contains(dato) || m.Moneda1.nombre.Contains(dato));
            }

            return View(denominacion.ToList());
        }

        // GET: Denominacions/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Denominacion denominacion = db.Denominacions.Find(id);
            if (denominacion == null)
            {
                return HttpNotFound();
            }
            return View(denominacion);
        }

        // GET: Denominacions/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre");
            return View();
        }

        // POST: Denominacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idDenominacion,moneda,valor,valorLetras,estado")] Denominacion denominacion)
        {
            if (ModelState.IsValid)
            {
                db.Denominacions.Add(denominacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre", denominacion.moneda);
            return View(denominacion);
        }

        // GET: Denominacions/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Denominacion denominacion = db.Denominacions.Find(id);
            if (denominacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre", denominacion.moneda);
            return View(denominacion);
        }

        // POST: Denominacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idDenominacion,moneda,valor,valorLetras,estado")] Denominacion denominacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(denominacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre", denominacion.moneda);
            return View(denominacion);
        }

        // GET: Denominacions/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Denominacion denominacion = db.Denominacions.Find(id);
            if (denominacion == null)
            {
                return HttpNotFound();
            }
            return View(denominacion);
        }

        // POST: Denominacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Denominacion denominacion = db.Denominacions.Find(id);
            db.Denominacions.Remove(denominacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult verReporte(string tipo, string busq)
        {
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptDenominaciones.rdlc");
            rpt.ReportPath = ruta;

            List<VwDenominacione> ls = new List<VwDenominacione>();

            var denominacion = from m in db.VwDenominaciones select m;

            if (!string.IsNullOrEmpty(busq))
            {
                denominacion = denominacion.Where(m => m.Valor.ToString().Contains(busq) || m.ValorLetras.Contains(busq) || m.Moneda.Contains(busq));
            }

            ls = denominacion.ToList();

            ReportDataSource rd = new ReportDataSource("VwDenominaciones", ls);
            rpt.DataSources.Add(rd);

            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }

        [Authorize]
        public ActionResult verReporteVertical(int? id)
        {
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptDenominacionesVerticales.rdlc");
            rpt.ReportPath = ruta;

            VwDenominacione g = db.VwDenominaciones.Find(id);
            List<VwDenominacione> ls = new List<VwDenominacione>();
            ls.Add(g);

            ReportDataSource rd = new ReportDataSource("DSDenominaciones", ls);
            rpt.DataSources.Add(rd);

            var b = rpt.Render("PDF", null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }


    }
}
