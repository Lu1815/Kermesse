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
    public class ArqueoCajaDetsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: ArqueoCajaDets
        [Authorize]
        public ActionResult Index()
        {
            var arqueoCajaDets = db.ArqueoCajaDets.Include(a => a.ArqueoCaja1).Include(a => a.Denominacion1).Include(a => a.Moneda1);
            return View(arqueoCajaDets.ToList());
        }

        // GET: ArqueoCajaDets/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCajaDet arqueoCajaDet = db.ArqueoCajaDets.Find(id);
            if (arqueoCajaDet == null)
            {
                return HttpNotFound();
            }
            return View(arqueoCajaDet);
        }

        // GET: ArqueoCajaDets/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.arqueoCaja = new SelectList(db.ArqueoCajas, "idArqueoCaja", "idArqueoCaja");
            ViewBag.denominacion = new SelectList(db.Denominacions, "idDenominacion", "valorLetras");
            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre");
            return View();
        }

        // POST: ArqueoCajaDets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idArqueoCajaDet,arqueoCaja,moneda,denominacion,cantidad,subtotal")] ArqueoCajaDet arqueoCajaDet)
        {
            if (ModelState.IsValid)
            {
                db.ArqueoCajaDets.Add(arqueoCajaDet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.arqueoCaja = new SelectList(db.ArqueoCajas, "idArqueoCaja", "idArqueoCaja", arqueoCajaDet.arqueoCaja);
            ViewBag.denominacion = new SelectList(db.Denominacions, "idDenominacion", "valorLetras", arqueoCajaDet.denominacion);
            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre", arqueoCajaDet.moneda);
            return View(arqueoCajaDet);
        }

        // GET: ArqueoCajaDets/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCajaDet arqueoCajaDet = db.ArqueoCajaDets.Find(id);
            if (arqueoCajaDet == null)
            {
                return HttpNotFound();
            }
            ViewBag.arqueoCaja = new SelectList(db.ArqueoCajas, "idArqueoCaja", "idArqueoCaja", arqueoCajaDet.arqueoCaja);
            ViewBag.denominacion = new SelectList(db.Denominacions, "idDenominacion", "valorLetras", arqueoCajaDet.denominacion);
            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre", arqueoCajaDet.moneda);
            return View(arqueoCajaDet);
        }

        // POST: ArqueoCajaDets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idArqueoCajaDet,arqueoCaja,moneda,denominacion,cantidad,subtotal")] ArqueoCajaDet arqueoCajaDet)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(arqueoCajaDet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.arqueoCaja = new SelectList(db.ArqueoCajas, "idArqueoCaja", "idArqueoCaja", arqueoCajaDet.arqueoCaja);
            ViewBag.denominacion = new SelectList(db.Denominacions, "idDenominacion", "valorLetras", arqueoCajaDet.denominacion);
            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre", arqueoCajaDet.moneda);
            return View(arqueoCajaDet);
        }

        // GET: ArqueoCajaDets/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCajaDet arqueoCajaDet = db.ArqueoCajaDets.Find(id);
            if (arqueoCajaDet == null)
            {
                return HttpNotFound();
            }
            return View(arqueoCajaDet);
        }

        // POST: ArqueoCajaDets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            ArqueoCajaDet arqueoCajaDet = db.ArqueoCajaDets.Find(id);
            db.ArqueoCajaDets.Remove(arqueoCajaDet);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptArqueoCajaDet.rdlc");
            rpt.ReportPath = ruta;

            List<VwArqueoCajaDet> ls = new List<VwArqueoCajaDet>();
            ls = db.VwArqueoCajaDets.ToList();

            ReportDataSource rd = new ReportDataSource("DSArqueoCajaDet", ls);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptArqueoCajaDetVertical.rdlc");
            rpt.ReportPath = ruta;

            VwArqueoCajaDet g = db.VwArqueoCajaDets.Find(id);
            List<VwArqueoCajaDet> ls = new List<VwArqueoCajaDet>();
            ls.Add(g);

            ReportDataSource rd = new ReportDataSource("DSArqueoCajaDet", ls);
            rpt.DataSources.Add(rd);

            var b = rpt.Render("PDF", null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }



    }
}
