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
    public class RolUsuariosController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: RolUsuarios
        [Authorize]
        public ActionResult Index(string dato)
        {
            var ru = from m in db.RolUsuarios select m;

            if (!string.IsNullOrEmpty(dato))
            {
                ru = ru.Where(m => m.Usuario1.userName.ToString().Contains(dato) || m.Rol1.rolDescripcion.Contains(dato));
            }

            return View(ru.ToList());
        }

        // GET: RolUsuarios/Details/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            RolUsuario rolUsuario = db.RolUsuarios.Find(id);
            db.RolUsuarios.Remove(rolUsuario);
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
        public ActionResult verReporte(string tipo, string busq)
        {

            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptRolUsuario.rdlc");
            rpt.ReportPath = ruta;

            List<VwRolUsuario> ls = new List<VwRolUsuario>();
            var ru = from m in db.VwRolUsuarios select m;

            if (!string.IsNullOrEmpty(busq))
            {
                ru = ru.Where(m => m.userName.ToString().Contains(busq) || m.rolDescripcion.Contains(busq));
            }

            ls = ru.ToList();

            ReportDataSource rd = new ReportDataSource("DSRolUsuario", ls);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptRolUsuarioVertical.rdlc");
            rpt.ReportPath = ruta;

            VwRolUsuario i = db.VwRolUsuarios.Find(id);
            List<VwRolUsuario> ls = new List<VwRolUsuario>();
            ls.Add(i);

            ReportDataSource rd = new ReportDataSource("DSRolUsuario", ls);
            rpt.DataSources.Add(rd);

            var b = rpt.Render("PDF", null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }
    }
}
