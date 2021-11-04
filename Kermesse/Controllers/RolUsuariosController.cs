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
    public class RolUsuariosController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: RolUsuarios
        public ActionResult Index(string dato)
        {
            var ru = from m in db.RolUsuarios select m;

            if (!string.IsNullOrEmpty(dato))
            {
                ru = ru.Where(m => m.usuario.ToString().Contains(dato));
            }

            return View(ru.ToList());
        }

        // GET: RolUsuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolUsuario rolUsuario = db.RolUsuarios.Find(id);
            if (rolUsuario == null)
            {
                return HttpNotFound();
            }
            return View(rolUsuario);
        }

        // GET: RolUsuarios/Create
        public ActionResult Create()
        {
            ViewBag.rol = new SelectList(db.Rols, "idRol", "rolDescripcion");
            ViewBag.usuario = new SelectList(db.Usuarios, "idUsuario", "userName");
            return View();
        }

        // POST: RolUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRolUsuario,usuario,rol")] RolUsuario rolUsuario)
        {
            if (ModelState.IsValid)
            {
                db.RolUsuarios.Add(rolUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.rol = new SelectList(db.Rols, "idRol", "rolDescripcion", rolUsuario.rol);
            ViewBag.usuario = new SelectList(db.Usuarios, "idUsuario", "userName", rolUsuario.usuario);
            return View(rolUsuario);
        }

        // GET: RolUsuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolUsuario rolUsuario = db.RolUsuarios.Find(id);
            if (rolUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.rol = new SelectList(db.Rols, "idRol", "rolDescripcion", rolUsuario.rol);
            ViewBag.usuario = new SelectList(db.Usuarios, "idUsuario", "userName", rolUsuario.usuario);
            return View(rolUsuario);
        }

        // POST: RolUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRolUsuario,usuario,rol")] RolUsuario rolUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rolUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rol = new SelectList(db.Rols, "idRol", "rolDescripcion", rolUsuario.rol);
            ViewBag.usuario = new SelectList(db.Usuarios, "idUsuario", "userName", rolUsuario.usuario);
            return View(rolUsuario);
        }

        // GET: RolUsuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolUsuario rolUsuario = db.RolUsuarios.Find(id);
            if (rolUsuario == null)
            {
                return HttpNotFound();
            }
            return View(rolUsuario);
        }

        // POST: RolUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RolUsuario rolUsuario = db.RolUsuarios.Find(id);
            db.RolUsuarios.Remove(rolUsuario);
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
