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
    public class ArqueoCajasController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: ArqueoCajas
        [Authorize]
        public ActionResult Index()
        {
            var arqueoCajas = db.ArqueoCajas.Include(a => a.Kermesse1).Include(a => a.Usuario).Include(a => a.Usuario1).Include(a => a.Usuario2);
            return View(arqueoCajas.ToList());
        }

        // GET: ArqueoCajas/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCaja arqueoCaja = db.ArqueoCajas.Find(id);
            if (arqueoCaja == null)
            {
                return HttpNotFound();
            }
            return View(arqueoCaja);
        }

        // GET: ArqueoCajas/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre");
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            return View();
        }

        // POST: ArqueoCajas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idArqueoCaja,kermesse,fechaArqueo,granTotal,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] ArqueoCaja arqueoCaja)
        {
            if (ModelState.IsValid)
            {
                db.ArqueoCajas.Add(arqueoCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", arqueoCaja.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioCreacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioModificacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioEliminacion);
            return View(arqueoCaja);
        }

        // GET: ArqueoCajas/Edit/5}
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCaja arqueoCaja = db.ArqueoCajas.Find(id);
            if (arqueoCaja == null)
            {
                return HttpNotFound();
            }
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", arqueoCaja.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioCreacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioModificacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioEliminacion);
            return View(arqueoCaja);
        }

        // POST: ArqueoCajas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idArqueoCaja,kermesse,fechaArqueo,granTotal,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] ArqueoCaja arqueoCaja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arqueoCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", arqueoCaja.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioCreacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioModificacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioEliminacion);
            return View(arqueoCaja);
        }

        // GET: ArqueoCajas/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCaja arqueoCaja = db.ArqueoCajas.Find(id);
            if (arqueoCaja == null)
            {
                return HttpNotFound();
            }
            return View(arqueoCaja);
        }

        // POST: ArqueoCajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            ArqueoCaja arqueoCaja = db.ArqueoCajas.Find(id);
            db.ArqueoCajas.Remove(arqueoCaja);
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
