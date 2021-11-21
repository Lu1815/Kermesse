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
    public class ListaPreciosController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: ListaPrecios
        [Authorize]
        public ActionResult Index(string dato)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            var lp = from m in db.ListaPrecios select m;

            if (!string.IsNullOrEmpty(dato))
            {
                lp = lp.Where(m => m.nombre.Contains(dato) || m.descripcion.Contains(dato) || m.Kermesse1.nombre.Contains(dato));
            }

            return View(lp.ToList());
        }

        // GET: ListaPrecios/Details/5
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
            ListaPrecio listaPrecio = db.ListaPrecios.Find(id);
            if (listaPrecio == null)
            {
                return HttpNotFound();
            }
            return View(listaPrecio);
        }

        // GET: ListaPrecios/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre");
            return View();
        }

        // POST: ListaPrecios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public ActionResult Create(ListaPrecio listaPrecio)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                ListaPrecio lp = new ListaPrecio();
                lp.nombre = listaPrecio.nombre;
                lp.descripcion = listaPrecio.descripcion;
                lp.kermesse = listaPrecio.kermesse;
                lp.estado = 1;
                db.ListaPrecios.Add(lp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", listaPrecio.kermesse);
            return View(listaPrecio);
        }

        // GET: ListaPrecios/Edit/5
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
            ListaPrecio listaPrecio = db.ListaPrecios.Find(id);
            if (listaPrecio == null)
            {
                return HttpNotFound();
            }
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", listaPrecio.kermesse);
            return View(listaPrecio);
        }

        // POST: ListaPrecios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idListaPrecio,kermesse,nombre,descripcion,estado")] ListaPrecio listaPrecio)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                listaPrecio.estado = 2;
                db.Entry(listaPrecio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", listaPrecio.kermesse);
            return View(listaPrecio);
        }

        // GET: ListaPrecios/Delete/5
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
            ListaPrecio listaPrecio = db.ListaPrecios.Find(id);
            if (listaPrecio == null)
            {
                return HttpNotFound();
            }
            return View(listaPrecio);
        }

        // POST: ListaPrecios/Delete/5
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

            ListaPrecio listaPrecio = db.ListaPrecios.Find(id);
            db.ListaPrecios.Remove(listaPrecio);
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
        public ActionResult VerReporte(string tipo, string busq)
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptListaPrecio1.rdlc");
            rpt.ReportPath = ruta;

            List<VwListaPrecio> ls = new List<VwListaPrecio>();

            var lp = from m in db.VwListaPrecios select m;

            if (!string.IsNullOrEmpty(busq))
            {
                lp = lp.Where(m => m.nombre.Contains(busq) || m.descripcion.Contains(busq) || m.kermesse.Contains(busq));
            }

            ls = lp.ToList();

            ReportDataSource rds = new ReportDataSource("DSListaPrecio", ls);

            rpt.DataSources.Add(rds);

            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);

            return new FileContentResult(b, mt);
        }

        [Authorize]
        public ActionResult VerReporteVertical(int? id)
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptListaPrecioVertical.rdlc");
            rpt.ReportPath = ruta;

            VwListaPrecio l = db.VwListaPrecios.Find(id);
            List<VwListaPrecio> ls = new List<VwListaPrecio>();

            ls.Add(l);

            ReportDataSource rds = new ReportDataSource("DSListaPrecio", ls);

            rpt.DataSources.Add(rds);

            var b = rpt.Render("PDF", null, out mt, out enc, out f, out s, out w);

            return new FileContentResult(b, mt);
        }

    }
}
