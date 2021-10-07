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
    public class CategoriaGastoesController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: CategoriaGastoes
        public ActionResult Index()
        {
            return View(db.CategoriaGastoes.ToList());
        }

        // GET: CategoriaGastoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaGasto categoriaGasto = db.CategoriaGastoes.Find(id);
            if (categoriaGasto == null)
            {
                return HttpNotFound();
            }
            return View(categoriaGasto);
        }

        // GET: CategoriaGastoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaGastoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCatGasto,nombreCategoria,descripcion,estado")] CategoriaGasto categoriaGasto)
        {
            if (ModelState.IsValid)
            {
                db.CategoriaGastoes.Add(categoriaGasto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriaGasto);
        }

        // GET: CategoriaGastoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaGasto categoriaGasto = db.CategoriaGastoes.Find(id);
            if (categoriaGasto == null)
            {
                return HttpNotFound();
            }
            return View(categoriaGasto);
        }

        // POST: CategoriaGastoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCatGasto,nombreCategoria,descripcion,estado")] CategoriaGasto categoriaGasto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriaGasto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriaGasto);
        }

        // GET: CategoriaGastoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaGasto categoriaGasto = db.CategoriaGastoes.Find(id);
            if (categoriaGasto == null)
            {
                return HttpNotFound();
            }
            return View(categoriaGasto);
        }

        // POST: CategoriaGastoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriaGasto categoriaGasto = db.CategoriaGastoes.Find(id);
            db.CategoriaGastoes.Remove(categoriaGasto);
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
