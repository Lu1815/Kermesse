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
    public class ListaPrecioDetsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: ListaPrecioDets
        [Authorize]
        public ActionResult Index(string dato)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            var listaPrecioDets = from m in db.ListaPrecioDets select m;

            if (!string.IsNullOrEmpty(dato))
            {
                listaPrecioDets = listaPrecioDets.Where(m => m.Producto1.nombre.Contains(dato) || m.precioVenta.ToString().Contains(dato));
            }

            return View(listaPrecioDets.ToList());
        }

        // GET: ListaPrecioDets/Details/5
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
            ListaPrecioDet listaPrecioDet = db.ListaPrecioDets.Find(id);
            if (listaPrecioDet == null)
            {
                return HttpNotFound();
            }
            return View(listaPrecioDet);
        }

        // GET: ListaPrecioDets/Create
        [Authorize]
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            ViewBag.listaPrecio = new SelectList(db.ListaPrecios, "idListaPrecio", "nombre");
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre");
            return View();
        }

        // POST: ListaPrecioDets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idListaPrecioDet,listaPrecio,producto,precioVenta")] ListaPrecioDet listaPrecioDet)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.ListaPrecioDets.Add(listaPrecioDet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.listaPrecio = new SelectList(db.ListaPrecios, "idListaPrecio", "nombre", listaPrecioDet.listaPrecio);
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre", listaPrecioDet.producto);
            return View(listaPrecioDet);
        }

        // GET: ListaPrecioDets/Edit/5
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
            ListaPrecioDet listaPrecioDet = db.ListaPrecioDets.Find(id);
            if (listaPrecioDet == null)
            {
                return HttpNotFound();
            }
            ViewBag.listaPrecio = new SelectList(db.ListaPrecios, "idListaPrecio", "nombre", listaPrecioDet.listaPrecio);
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre", listaPrecioDet.producto);
            return View(listaPrecioDet);
        }

        // POST: ListaPrecioDets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idListaPrecioDet,listaPrecio,producto,precioVenta")] ListaPrecioDet listaPrecioDet)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(listaPrecioDet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.listaPrecio = new SelectList(db.ListaPrecios, "idListaPrecio", "nombre", listaPrecioDet.listaPrecio);
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre", listaPrecioDet.producto);
            return View(listaPrecioDet);
        }

        // GET: ListaPrecioDets/Delete/5
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
            ListaPrecioDet listaPrecioDet = db.ListaPrecioDets.Find(id);
            if (listaPrecioDet == null)
            {
                return HttpNotFound();
            }
            return View(listaPrecioDet);
        }

        // POST: ListaPrecioDets/Delete/5
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

            ListaPrecioDet listaPrecioDet = db.ListaPrecioDets.Find(id);
            db.ListaPrecioDets.Remove(listaPrecioDet);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptListaPrecioDet.rdlc");
            rpt.ReportPath = ruta;

            List<VwListaPrecioDet> ls = new List<VwListaPrecioDet>();

            var listaPrecioDets = from m in db.VwListaPrecioDets select m;

            if (!string.IsNullOrEmpty(busq))
            {
                listaPrecioDets = listaPrecioDets.Where(m => m.precioVenta.ToString().Contains(busq) || m.producto.Contains(busq));
            }

            ls = listaPrecioDets.ToList();

            ReportDataSource rds = new ReportDataSource("DSListaPrecioDet", ls);

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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptListaPrecioDetVertical.rdlc");
            rpt.ReportPath = ruta;

            VwListaPrecioDet l = db.VwListaPrecioDets.Find(id);
            List<VwListaPrecioDet> ls = new List<VwListaPrecioDet>();

            ls.Add(l);

            ReportDataSource rds = new ReportDataSource("DSListaPrecioDet", ls);

            rpt.DataSources.Add(rds);

            var b = rpt.Render("PDF", null, out mt, out enc, out f, out s, out w);

            return new FileContentResult(b, mt);
        }
    }
}
