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
    public class IngresoComunidadDetsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: IngresoComunidadDets
        [Authorize]
        public ActionResult Index(string dato)
        {
            var icd = from m in db.IngresoComunidadDets select m;

            if (!string.IsNullOrEmpty(dato))
            {
                icd = icd.Where(m => m.denominacion.Contains(dato));
            }

            return View(icd.ToList());
        }

        // GET: IngresoComunidadDets/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngresoComunidadDet ingresoComunidadDet = db.IngresoComunidadDets.Find(id);
            if (ingresoComunidadDet == null)
            {
                return HttpNotFound();
            }
            return View(ingresoComunidadDet);
        }

        // GET: IngresoComunidadDets/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.bono = new SelectList(db.ControlBonoes, "idBono", "nombre");
            ViewBag.ingresoComunidad = new SelectList(db.IngresoComunidads, "idIngresoComunidad", "idIngresoComunidad");
            return View();
        }

        // POST: IngresoComunidadDets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idIngresoComunidadDet,ingresoComunidad,bono,denominacion,cantidad,subTotalBono")] IngresoComunidadDet ingresoComunidadDet)
        {
            if (ModelState.IsValid)
            {
                db.IngresoComunidadDets.Add(ingresoComunidadDet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bono = new SelectList(db.ControlBonoes, "idBono", "nombre", ingresoComunidadDet.bono);
            ViewBag.ingresoComunidad = new SelectList(db.IngresoComunidads, "idIngresoComunidad", "idIngresoComunidad", ingresoComunidadDet.ingresoComunidad);
            return View(ingresoComunidadDet);
        }

        // GET: IngresoComunidadDets/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngresoComunidadDet ingresoComunidadDet = db.IngresoComunidadDets.Find(id);
            if (ingresoComunidadDet == null)
            {
                return HttpNotFound();
            }
            ViewBag.bono = new SelectList(db.ControlBonoes, "idBono", "nombre", ingresoComunidadDet.bono);
            ViewBag.ingresoComunidad = new SelectList(db.IngresoComunidads, "idIngresoComunidad", "idIngresoComunidad", ingresoComunidadDet.ingresoComunidad);
            return View(ingresoComunidadDet);
        }

        // POST: IngresoComunidadDets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idIngresoComunidadDet,ingresoComunidad,bono,denominacion,cantidad,subTotalBono")] IngresoComunidadDet ingresoComunidadDet)
        {
            System.Diagnostics.Debug.WriteLine("ENTRÓ PUTO");
            IngresoComunidadDet i = db.IngresoComunidadDets.Find(ingresoComunidadDet.idIngresoComunidadDet);

            i.ingresoComunidad = ingresoComunidadDet.ingresoComunidad;
            i.bono = ingresoComunidadDet.bono;
            i.denominacion = ingresoComunidadDet.denominacion;
            i.cantidad = ingresoComunidadDet.cantidad;
            i.subTotalBono = ingresoComunidadDet.subTotalBono;

            if (ModelState.IsValid)
            {
                db.Entry(i).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bono = new SelectList(db.ControlBonoes, "idBono", "nombre", ingresoComunidadDet.bono);
            ViewBag.ingresoComunidad = new SelectList(db.IngresoComunidads, "idIngresoComunidad", "idIngresoComunidad", ingresoComunidadDet.ingresoComunidad);
            return View(ingresoComunidadDet);
        }

        // GET: IngresoComunidadDets/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngresoComunidadDet ingresoComunidadDet = db.IngresoComunidadDets.Find(id);
            if (ingresoComunidadDet == null)
            {
                return HttpNotFound();
            }
            return View(ingresoComunidadDet);
        }

        // POST: IngresoComunidadDets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            IngresoComunidadDet ingresoComunidadDet = db.IngresoComunidadDets.Find(id);
            db.IngresoComunidadDets.Remove(ingresoComunidadDet);
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
    }
}
