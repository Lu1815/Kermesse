using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kermesse.Models;

namespace Kermesse.Controllers
{
    public class DenominacionsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: Denominacions
        public ActionResult Index(string dato)
        {
            var denominacion = from m in db.Denominacions select m;

            if (!string.IsNullOrEmpty(dato))
            {
                denominacion = denominacion.Where(m => m.valor.ToString().Contains(dato) || m.valorLetras.Contains(dato));
            }

            return View(denominacion.ToList());
        }

        // GET: Denominacions/Details/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            Denominacion denominacion = db.Denominacions.Find(id);
            db.Denominacions.Remove(denominacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
