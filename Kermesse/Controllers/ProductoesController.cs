﻿using System;
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
    public class ProductoesController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        [Authorize]
        // GET: Productoes
        public ActionResult Index(string dato)
        {
            var prod = from m in db.Productoes select m;

            if (!string.IsNullOrEmpty(dato))
            {
                prod = prod.Where(m => m.nombre.Contains(dato) || m.descripcion.Contains(dato));
            }

            return View(prod.ToList());
        }

        [Authorize]
        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        [Authorize]
        // GET: Productoes/Create
        public ActionResult Create()
        {
            ViewBag.catProd = new SelectList(db.CategoriaProductoes, "idCatProd", "nombre");
            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idProducto,comunidad,catProd,nombre,descripcion,cantidad,precioVSugerido,estado")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productoes.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.catProd = new SelectList(db.CategoriaProductoes, "idCatProd", "nombre", producto.catProd);
            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre", producto.comunidad);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.catProd = new SelectList(db.CategoriaProductoes, "idCatProd", "nombre", producto.catProd);
            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre", producto.comunidad);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idProducto,comunidad,catProd,nombre,descripcion,cantidad,precioVSugerido,estado")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.catProd = new SelectList(db.CategoriaProductoes, "idCatProd", "nombre", producto.catProd);
            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre", producto.comunidad);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productoes.Find(id);
            db.Productoes.Remove(producto);
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
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptProductos.rdlc");
            rpt.ReportPath = ruta;

            List<VwProducto> ls = new List<VwProducto>();

            ls = db.VwProductos.ToList();

            ReportDataSource rds = new ReportDataSource("DSProductos", ls);

            rpt.DataSources.Add(rds);

            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);

            return new FileContentResult(b, mt);
        }

        [Authorize]
        public ActionResult VerReporteVertical(int? id)
        {
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptProductosVertical.rdlc");
            rpt.ReportPath = ruta;

            VwProducto l = db.VwProductos.Find(id);
            List<VwProducto> ls = new List<VwProducto>();

            ls.Add(l);

            ReportDataSource rds = new ReportDataSource("DSProductos", ls);

            rpt.DataSources.Add(rds);

            var b = rpt.Render("PDF", null, out mt, out enc, out f, out s, out w);

            return new FileContentResult(b, mt);
        }
    }
}
