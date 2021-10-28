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
    public class ListaPreciosController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: ListaPrecios
        public ActionResult Index()
        {
            var listaPrecios = db.ListaPrecios.Include(l => l.Kermesse1);
            return View(listaPrecios.ToList());
        }

        // GET: ListaPrecios/Details/5
        public ActionResult Details(int? id)
        {
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
        public ActionResult Create()
        {
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre");
            return View();
        }

        // POST: ListaPrecios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idListaPrecio,kermesse,nombre,descripcion,estado")] ListaPrecio listaPrecio)
        {
            if (ModelState.IsValid)
            {
                db.ListaPrecios.Add(listaPrecio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", listaPrecio.kermesse);
            return View(listaPrecio);
        }

        // GET: ListaPrecios/Edit/5
        public ActionResult Edit(int? id)
        {
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
        public ActionResult Edit([Bind(Include = "idListaPrecio,kermesse,nombre,descripcion,estado")] ListaPrecio listaPrecio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listaPrecio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", listaPrecio.kermesse);
            return View(listaPrecio);
        }

        // GET: ListaPrecios/Delete/5
        public ActionResult Delete(int? id)
        {
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
        public ActionResult DeleteConfirmed(int id)
        {
            ListaPrecio listaPrecio = db.ListaPrecios.Find(id);
            db.ListaPrecios.Remove(listaPrecio);
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

        public ActionResult VerReporte(string tipo)
        {
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptListaPrecio1.rdlc");
            rpt.ReportPath = ruta;

            List<VwListaPrecio> ls = new List<VwListaPrecio>();

            ls = db.VwListaPrecios.ToList();

            ReportDataSource rds = new ReportDataSource("DSListaPrecio", ls);

            rpt.DataSources.Add(rds);

            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);

            return new FileContentResult(b, mt);
        }
    }
}
