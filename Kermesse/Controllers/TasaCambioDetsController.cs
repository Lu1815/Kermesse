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
    public class TasaCambioDetsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: TasaCambioDets
        [Authorize]
        public ActionResult Index(string dato)
        {
            var tcd = from m in db.TasaCambioDets select m;

            if (!string.IsNullOrEmpty(dato))
            {
                tcd = tcd.Where(m => m.tipoCambio.ToString().Contains(dato) || m.fecha.ToString().Contains(dato));
            }

            return View(tcd.ToList());
        }

        // GET: TasaCambioDets/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasaCambioDet tasaCambioDet = db.TasaCambioDets.Find(id);
            if (tasaCambioDet == null)
            {
                return HttpNotFound();
            }
            return View(tasaCambioDet);
        }

        // GET: TasaCambioDets/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.tasaCambio = new SelectList(db.TasaCambios, "idTasaCambio", "mes");
            return View();
        }

        // POST: TasaCambioDets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idTasaCambioDet,tasaCambio,fecha,tipoCambio,estado")] TasaCambioDet tasaCambioDet)
        {
            if (ModelState.IsValid)
            {
                db.TasaCambioDets.Add(tasaCambioDet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tasaCambio = new SelectList(db.TasaCambios, "idTasaCambio", "mes", tasaCambioDet.tasaCambio);
            return View(tasaCambioDet);
        }

        // GET: TasaCambioDets/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasaCambioDet tasaCambioDet = db.TasaCambioDets.Find(id);
            if (tasaCambioDet == null)
            {
                return HttpNotFound();
            }
            ViewBag.tasaCambio = new SelectList(db.TasaCambios, "idTasaCambio", "mes", tasaCambioDet.tasaCambio);
            return View(tasaCambioDet);
        }

        // POST: TasaCambioDets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idTasaCambioDet,tasaCambio,fecha,tipoCambio,estado")] TasaCambioDet tasaCambioDet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasaCambioDet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tasaCambio = new SelectList(db.TasaCambios, "idTasaCambio", "mes", tasaCambioDet.tasaCambio);
            return View(tasaCambioDet);
        }

        // GET: TasaCambioDets/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasaCambioDet tasaCambioDet = db.TasaCambioDets.Find(id);
            if (tasaCambioDet == null)
            {
                return HttpNotFound();
            }
            return View(tasaCambioDet);
        }

        // POST: TasaCambioDets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            TasaCambioDet tasaCambioDet = db.TasaCambioDets.Find(id);
            db.TasaCambioDets.Remove(tasaCambioDet);
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
        public ActionResult VerReporte(string tipo)
        {
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptTasaCambioDets.rdlc");
            rpt.ReportPath = ruta;

            List<TasaCambioDet> ls = new List<TasaCambioDet>();

            ls = db.TasaCambioDets.ToList();

            ReportDataSource rds = new ReportDataSource("DSTasaCambioDets", ls);

            rpt.DataSources.Add(rds);

            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);

            return new FileContentResult(b, mt);
        }


    }
}
