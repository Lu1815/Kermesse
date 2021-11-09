
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
    public class TasaCambiosController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: TasaCambios
        [Authorize]
        public ActionResult Index(string dato)
        {
            var tc = from m in db.TasaCambios select m;

            if (!string.IsNullOrEmpty(dato))
            {
                tc = tc.Where(m => m.mes.Contains(dato) || m.anio.ToString().Contains(dato) || m.monedaO.ToString().Contains(dato) || m.monedaC.ToString().Contains(dato));
            }

            return View(tc.ToList());
        }

        // GET: TasaCambios/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasaCambio tasaCambio = db.TasaCambios.Find(id);
            if (tasaCambio == null)
            {
                return HttpNotFound();
            }
            return View(tasaCambio);
        }

        // GET: TasaCambios/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.monedaO = new SelectList(db.Monedas, "idMoneda", "nombre");
            ViewBag.monedaC = new SelectList(db.Monedas, "idMoneda", "nombre");
            return View();
        }

        // POST: TasaCambios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(TasaCambio tasaCambio)
        {
            if (ModelState.IsValid)
            {
                TasaCambio ts = new TasaCambio();
                ts.monedaO = tasaCambio.monedaO;
                ts.monedaC = tasaCambio.monedaC;
                ts.mes = tasaCambio.mes;
                ts.anio = tasaCambio.anio;
                ts.estado = 1;
                db.TasaCambios.Add(ts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.monedaO = new SelectList(db.Monedas, "idMoneda", "nombre", tasaCambio.monedaO);
            ViewBag.monedaC = new SelectList(db.Monedas, "idMoneda", "nombre", tasaCambio.monedaC);
            return View(tasaCambio);
        }

        // GET: TasaCambios/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasaCambio tasaCambio = db.TasaCambios.Find(id);
            if (tasaCambio == null)
            {
                return HttpNotFound();
            }
            ViewBag.monedaO = new SelectList(db.Monedas, "idMoneda", "nombre", tasaCambio.monedaO);
            ViewBag.monedaC = new SelectList(db.Monedas, "idMoneda", "nombre", tasaCambio.monedaC);
            return View(tasaCambio);
        }

        // POST: TasaCambios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idTasaCambio,monedaO,monedaC,mes,anio,estado")] TasaCambio tasaCambio)
        {
            if (ModelState.IsValid)
            {
                tasaCambio.estado = 2;
                db.Entry(tasaCambio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.monedaO = new SelectList(db.Monedas, "idMoneda", "nombre", tasaCambio.monedaO);
            ViewBag.monedaC = new SelectList(db.Monedas, "idMoneda", "nombre", tasaCambio.monedaC);
            return View(tasaCambio);
        }

        // GET: TasaCambios/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasaCambio tasaCambio = db.TasaCambios.Find(id);
            if (tasaCambio == null)
            {
                return HttpNotFound();
            }
            return View(tasaCambio);
        }

        // POST: TasaCambios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            TasaCambio tasaCambio = db.TasaCambios.Find(id);
            db.TasaCambios.Remove(tasaCambio);
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
        public ActionResult verReporte(string tipo)
        {
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptTasaCambios.rdlc");
            rpt.ReportPath = ruta;

            List<TasaCambio> ls = new List<TasaCambio>();
            ls = db.TasaCambios.ToList();

            ReportDataSource rd = new ReportDataSource("DSTasaCambios", ls);
            rpt.DataSources.Add(rd);

            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }
    }
}
