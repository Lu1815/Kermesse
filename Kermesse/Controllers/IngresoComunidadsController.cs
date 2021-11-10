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
    public class IngresoComunidadsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: IngresoComunidads
        [Authorize]
        public ActionResult Index(string dato)
        {
            var ic = from m in db.IngresoComunidads select m;

            if (!string.IsNullOrEmpty(dato))
            {

                ic = ic.Where(m => m.cantProducto.ToString().Contains(dato));
            }

            return View(ic.ToList());
        }

        // GET: IngresoComunidads/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre");
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre");
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre");
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            return View();
        }

        // POST: IngresoComunidads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idIngresoComunidad,kermesse,comunidad,producto,cantProducto,totalBonos,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] IngresoComunidad ingresoComunidad)
        {

            if (ModelState.IsValid)
            {
                ingresoComunidad.usuarioCreacion = int.Parse(Session["UserID"].ToString());
                ingresoComunidad.fechaCreacion = DateTime.Now;
                db.IngresoComunidads.Add(ingresoComunidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre", ingresoComunidad.comunidad);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", ingresoComunidad.kermesse);
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre", ingresoComunidad.producto);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", ingresoComunidad.usuarioCreacion);            
            return View(ingresoComunidad);
        }

        // GET: IngresoComunidads/Edit/5
        [Authorize]
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
            return View(ingresoComunidad);
        }

        // POST: IngresoComunidads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idIngresoComunidad,kermesse,comunidad,producto,cantProducto,totalBonos,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] IngresoComunidad ingresoComunidad)
        {

            IngresoComunidad i = db.IngresoComunidads.Find(ingresoComunidad.idIngresoComunidad);

            i.kermesse = ingresoComunidad.kermesse;
            i.comunidad = ingresoComunidad.comunidad;
            i.producto = ingresoComunidad.producto;
            i.cantProducto = ingresoComunidad.cantProducto;
            i.totalBonos = ingresoComunidad.totalBonos;
            i.usuarioModificacion = int.Parse(Session["UserID"].ToString());
            i.fechaModificacion = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(i).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre", ingresoComunidad.comunidad);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", ingresoComunidad.kermesse);
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre", ingresoComunidad.producto);
            
            return View(ingresoComunidad);
        }

        // GET: IngresoComunidads/Delete/5
        [Authorize]
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
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            IngresoComunidad ingresoComunidad = db.IngresoComunidads.Find(id);
            db.IngresoComunidads.Remove(ingresoComunidad);
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
