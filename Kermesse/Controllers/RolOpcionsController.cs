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
    public class RolOpcionsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: RolOpcions
        [Authorize]
        public ActionResult Index(string dato)
        {
            var RolOpcions = from m in db.RolOpcions select m;

            if (!string.IsNullOrEmpty(dato))
            {
                RolOpcions = RolOpcions.Where(m => m.rol.ToString().Contains(dato) || m.opcion.ToString().Contains(dato));
            }

            return View(RolOpcions.ToList());
        }

        // GET: RolOpcions/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolOpcion rolOpcion = db.RolOpcions.Find(id);
            if (rolOpcion == null)
            {
                return HttpNotFound();
            }
            return View(rolOpcion);
        }

        // GET: RolOpcions/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.opcion = new SelectList(db.Opcions, "idOpcion", "opcionDescripcion");
            ViewBag.rol = new SelectList(db.Rols, "idRol", "rolDescripcion");
            return View();
        }

        // POST: RolOpcions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idRolOpcion,rol,opcion")] RolOpcion rolOpcion)
        {
            if (ModelState.IsValid)
            {
                db.RolOpcions.Add(rolOpcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.opcion = new SelectList(db.Opcions, "idOpcion", "opcionDescripcion", rolOpcion.opcion);
            ViewBag.rol = new SelectList(db.Rols, "idRol", "rolDescripcion", rolOpcion.rol);
            return View(rolOpcion);
        }

        // GET: RolOpcions/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolOpcion rolOpcion = db.RolOpcions.Find(id);
            if (rolOpcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.opcion = new SelectList(db.Opcions, "idOpcion", "opcionDescripcion", rolOpcion.opcion);
            ViewBag.rol = new SelectList(db.Rols, "idRol", "rolDescripcion", rolOpcion.rol);
            return View(rolOpcion);
        }

        // POST: RolOpcions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idRolOpcion,rol,opcion")] RolOpcion rolOpcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rolOpcion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.opcion = new SelectList(db.Opcions, "idOpcion", "opcionDescripcion", rolOpcion.opcion);
            ViewBag.rol = new SelectList(db.Rols, "idRol", "rolDescripcion", rolOpcion.rol);
            return View(rolOpcion);
        }

        // GET: RolOpcions/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolOpcion rolOpcion = db.RolOpcions.Find(id);
            if (rolOpcion == null)
            {
                return HttpNotFound();
            }
            return View(rolOpcion);
        }

        // POST: RolOpcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            RolOpcion rolOpcion = db.RolOpcions.Find(id);
            db.RolOpcions.Remove(rolOpcion);
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
