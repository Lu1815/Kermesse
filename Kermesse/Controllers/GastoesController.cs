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
    public class GastoesController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: Gastoes
        [Authorize]
        public ActionResult Index(string dato)
        {
            var gasto = from m in db.Gastoes select m;

            if (!string.IsNullOrEmpty(dato))
            {
                gasto = gasto.Where(m => m.concepto.Contains(dato) || m.monto.ToString().Contains(dato));
            }

            return View(gasto.ToList());
        }

        // GET: Gastoes/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gasto gasto = db.Gastoes.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            return View(gasto);
        }

        // GET: Gastoes/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.catGasto = new SelectList(db.CategoriaGastoes, "idCatGasto", "nombreCategoria");
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre");
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            return View();
        }

        // POST: Gastoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idGasto,kermesse,catGasto,fechGasto,concepto,monto,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] Gasto gasto)
        {

            gasto.usuarioCreacion = int.Parse(Session["UserID"].ToString(), System.Globalization.NumberStyles.Integer);
            gasto.fechaCreacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Gastoes.Add(gasto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.catGasto = new SelectList(db.CategoriaGastoes, "idCatGasto", "nombreCategoria", gasto.catGasto);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", gasto.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioCreacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioEliminacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioModificacion);
            return View(gasto);
        }

        // GET: Gastoes/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gasto gasto = db.Gastoes.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            ViewBag.catGasto = new SelectList(db.CategoriaGastoes, "idCatGasto", "nombreCategoria", gasto.catGasto);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", gasto.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioCreacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioEliminacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioModificacion);
            return View(gasto);
        }

        // POST: Gastoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idGasto,kermesse,catGasto,fechGasto,concepto,monto,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gasto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.catGasto = new SelectList(db.CategoriaGastoes, "idCatGasto", "nombreCategoria", gasto.catGasto);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", gasto.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioCreacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioEliminacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioModificacion);
            return View(gasto);
        }

        // GET: Gastoes/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gasto gasto = db.Gastoes.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            return View(gasto);
        }

        // POST: Gastoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Gasto gasto = db.Gastoes.Find(id);
            db.Gastoes.Remove(gasto);
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
