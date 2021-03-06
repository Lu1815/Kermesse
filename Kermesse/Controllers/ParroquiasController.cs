using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kermesse.Models;
using Microsoft.Reporting.WebForms;

namespace Kermesse.Controllers
{
    public class ParroquiasController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: Parroquias
        [Authorize]
        public ActionResult Index(string dato)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            var parroquia = from p in db.Parroquias select p;

            if (!string.IsNullOrEmpty(dato))
            {
                parroquia = parroquia.Where(p => p.nombre.Contains(dato) || p.direccion.Contains(dato) || p.telefono.Contains(dato) || p.sitioWeb.Contains(dato) || p.parroco.Contains(dato));
            }

            return View(parroquia.ToList());
        }

        // GET: Parroquias/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parroquia parroquia = db.Parroquias.Find(id);
            if (parroquia == null)
            {
                return HttpNotFound();
            }
            return View(parroquia);
        }

        // GET: Parroquias/Create
        [Authorize]
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Parroquias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idParroquia,nombre,direccion,telefono,parroco,logo,sitioWeb")] Parroquia parroquia)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Parroquias.Add(parroquia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parroquia);
        }

        // GET: Parroquias/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parroquia parroquia = db.Parroquias.Find(id);
            if (parroquia == null)
            {
                return HttpNotFound();
            }
            return View(parroquia);
        }

        // POST: Parroquias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idParroquia,nombre,direccion,telefono,parroco,logo,sitioWeb")] Parroquia parroquia)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(parroquia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parroquia);
        }

        // GET: Parroquias/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parroquia parroquia = db.Parroquias.Find(id);
            if (parroquia == null)
            {
                return HttpNotFound();
            }
            return View(parroquia);
        }

        // POST: Parroquias/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            Parroquia parroquia = db.Parroquias.Find(id);
            db.Parroquias.Remove(parroquia);
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
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptParroquia1.rdlc");
            rpt.ReportPath = ruta;

            List<Parroquia> ls = new List<Parroquia>();

            ls = db.Parroquias.ToList();

            ReportDataSource rds = new ReportDataSource("DSParroquia", ls);

            rpt.DataSources.Add(rds);

            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);

            return new FileContentResult(b, mt);
        }
    }
}
