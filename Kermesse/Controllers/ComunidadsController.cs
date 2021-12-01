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
    public class ComunidadsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: Comunidads
        [Authorize]
        public ActionResult Index(string dato)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            var comunidad = from m in db.Comunidads select m;

            if (!string.IsNullOrEmpty(dato))
            {
                comunidad = comunidad.Where(m => m.nombre.Contains(dato) || m.responsble.Contains(dato));
            }

            return View(comunidad.ToList());
        }

        // GET: Comunidads/Details/5
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
            Comunidad comunidad = db.Comunidads.Find(id);
            if (comunidad == null)
            {
                return HttpNotFound();
            }
            return View(comunidad);
        }

        // GET: Comunidads/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comunidads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idComunidad,nombre,responsble,descContribucion,estado")] Comunidad comunidad)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Comunidads.Add(comunidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comunidad);
        }

        // GET: Comunidads/Edit/5
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
            Comunidad comunidad = db.Comunidads.Find(id);
            if (comunidad == null)
            {
                return HttpNotFound();
            }
            return View(comunidad);
        }

        // POST: Comunidads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idComunidad,nombre,responsble,descContribucion,estado")] Comunidad comunidad)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(comunidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comunidad);
        }

        // GET: Comunidads/Delete/5
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
            Comunidad comunidad = db.Comunidads.Find(id);
            if (comunidad == null)
            {
                return HttpNotFound();
            }
            return View(comunidad);
        }

        // POST: Comunidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            Comunidad comunidad = db.Comunidads.Find(id);
            db.Comunidads.Remove(comunidad);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptComunidades.rdlc");
            rpt.ReportPath = ruta;

            List<Comunidad> ls = new List<Comunidad>();
            

            var comunidad = from m in db.Comunidads select m;

            if (!string.IsNullOrEmpty(busq))
            {
                comunidad = comunidad.Where(m => m.nombre.Contains(busq) || m.responsble.Contains(busq));
            }

            ls = comunidad.ToList();

            ReportDataSource rd = new ReportDataSource("DSComunidades", ls);
            rpt.DataSources.Add(rd);

            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }


        [Authorize]
        public ActionResult verReporteVertical(int? id)
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptComunidadesVertical.rdlc");
            rpt.ReportPath = ruta;

            Comunidad g = db.Comunidads.Find(id);
            List<Comunidad> ls = new List<Comunidad>();
            ls.Add(g);

            ReportDataSource rd = new ReportDataSource("DSComunidades", ls);
            rpt.DataSources.Add(rd);

            var b = rpt.Render("PDF", null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }


        public ActionResult verReporteResumen(string tipo)
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptIngresosYEgresos.rdlc");
            rpt.ReportPath = ruta;

            List<VwIngresoComunidadDet> ls = new List<VwIngresoComunidadDet>();
            var ic = from m in db.VwIngresoComunidadDets select m;
            ls = ic.ToList();
            List<VwGasto> ls1 = new List<VwGasto>();
            var ic1 = from m in db.VwGastoes select m;
            ls1 = ic1.ToList();

            ReportDataSource rd = new ReportDataSource("DSIngresos", ls);
            rpt.DataSources.Add(rd);
            ReportDataSource rd1 = new ReportDataSource("DSEgresos", ls);
            rpt.DataSources.Add(rd1);

            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }

    }
}
