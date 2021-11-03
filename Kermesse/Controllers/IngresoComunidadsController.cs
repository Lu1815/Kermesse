﻿using System;
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
    public class IngresoComunidadsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: IngresoComunidads
        public ActionResult Index()
        {
            var ingresoComunidads = db.IngresoComunidads.Include(i => i.Comunidad1).Include(i => i.Kermesse1).Include(i => i.Producto1).Include(i => i.Usuario).Include(i => i.Usuario1).Include(i => i.Usuario2);
            return View(ingresoComunidads.ToList());
        }

        // GET: IngresoComunidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngresoComunidad ingresoComunidad = db.IngresoComunidads.Find(id);
            if (ingresoComunidad == null)
            {
                return HttpNotFound();
            }
            return View(ingresoComunidad);
        }

        // GET: IngresoComunidads/Create
        public ActionResult Create()
        {
            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre");
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre");
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre");
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            return View();
        }

        // POST: IngresoComunidads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idIngresoComunidad,kermesse,comunidad,producto,cantProducto,totalBonos,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] IngresoComunidad ingresoComunidad)
        {
            if (ModelState.IsValid)
            {
                db.IngresoComunidads.Add(ingresoComunidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre", ingresoComunidad.comunidad);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", ingresoComunidad.kermesse);
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre", ingresoComunidad.producto);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", ingresoComunidad.usuarioCreacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", ingresoComunidad.usuarioModificacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", ingresoComunidad.usuarioEliminacion);
            return View(ingresoComunidad);
        }

        // GET: IngresoComunidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngresoComunidad ingresoComunidad = db.IngresoComunidads.Find(id);
            if (ingresoComunidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre", ingresoComunidad.comunidad);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", ingresoComunidad.kermesse);
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre", ingresoComunidad.producto);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", ingresoComunidad.usuarioCreacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", ingresoComunidad.usuarioModificacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", ingresoComunidad.usuarioEliminacion);
            return View(ingresoComunidad);
        }

        // POST: IngresoComunidads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idIngresoComunidad,kermesse,comunidad,producto,cantProducto,totalBonos,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] IngresoComunidad ingresoComunidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingresoComunidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre", ingresoComunidad.comunidad);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", ingresoComunidad.kermesse);
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre", ingresoComunidad.producto);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", ingresoComunidad.usuarioCreacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", ingresoComunidad.usuarioModificacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", ingresoComunidad.usuarioEliminacion);
            return View(ingresoComunidad);
        }

        // GET: IngresoComunidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngresoComunidad ingresoComunidad = db.IngresoComunidads.Find(id);
            if (ingresoComunidad == null)
            {
                return HttpNotFound();
            }
            return View(ingresoComunidad);
        }

        // POST: IngresoComunidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IngresoComunidad ingresoComunidad = db.IngresoComunidads.Find(id);
            db.IngresoComunidads.Remove(ingresoComunidad);
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
