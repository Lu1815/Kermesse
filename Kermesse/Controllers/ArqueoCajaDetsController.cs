﻿using System;
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
    public class ArqueoCajaDetsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: ArqueoCajaDets
        public ActionResult Index()
        {
            var arqueoCajaDets = db.ArqueoCajaDets.Include(a => a.ArqueoCaja1).Include(a => a.Denominacion1).Include(a => a.Moneda1);
            return View(arqueoCajaDets.ToList());
        }

        // GET: ArqueoCajaDets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCajaDet arqueoCajaDet = db.ArqueoCajaDets.Find(id);
            if (arqueoCajaDet == null)
            {
                return HttpNotFound();
            }
            return View(arqueoCajaDet);
        }

        // GET: ArqueoCajaDets/Create
        public ActionResult Create()
        {
            ViewBag.arqueoCaja = new SelectList(db.ArqueoCajas, "idArqueoCaja", "idArqueoCaja");
            ViewBag.denominacion = new SelectList(db.Denominacions, "idDenominacion", "valorLetras");
            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre");
            return View();
        }

        // POST: ArqueoCajaDets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idArqueoCajaDet,arqueoCaja,moneda,denominacion,cantidad,subtotal")] ArqueoCajaDet arqueoCajaDet)
        {
            if (ModelState.IsValid)
            {
                db.ArqueoCajaDets.Add(arqueoCajaDet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.arqueoCaja = new SelectList(db.ArqueoCajas, "idArqueoCaja", "idArqueoCaja", arqueoCajaDet.arqueoCaja);
            ViewBag.denominacion = new SelectList(db.Denominacions, "idDenominacion", "valorLetras", arqueoCajaDet.denominacion);
            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre", arqueoCajaDet.moneda);
            return View(arqueoCajaDet);
        }

        // GET: ArqueoCajaDets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCajaDet arqueoCajaDet = db.ArqueoCajaDets.Find(id);
            if (arqueoCajaDet == null)
            {
                return HttpNotFound();
            }
            ViewBag.arqueoCaja = new SelectList(db.ArqueoCajas, "idArqueoCaja", "idArqueoCaja", arqueoCajaDet.arqueoCaja);
            ViewBag.denominacion = new SelectList(db.Denominacions, "idDenominacion", "valorLetras", arqueoCajaDet.denominacion);
            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre", arqueoCajaDet.moneda);
            return View(arqueoCajaDet);
        }

        // POST: ArqueoCajaDets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idArqueoCajaDet,arqueoCaja,moneda,denominacion,cantidad,subtotal")] ArqueoCajaDet arqueoCajaDet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arqueoCajaDet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.arqueoCaja = new SelectList(db.ArqueoCajas, "idArqueoCaja", "idArqueoCaja", arqueoCajaDet.arqueoCaja);
            ViewBag.denominacion = new SelectList(db.Denominacions, "idDenominacion", "valorLetras", arqueoCajaDet.denominacion);
            ViewBag.moneda = new SelectList(db.Monedas, "idMoneda", "nombre", arqueoCajaDet.moneda);
            return View(arqueoCajaDet);
        }

        // GET: ArqueoCajaDets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCajaDet arqueoCajaDet = db.ArqueoCajaDets.Find(id);
            if (arqueoCajaDet == null)
            {
                return HttpNotFound();
            }
            return View(arqueoCajaDet);
        }

        // POST: ArqueoCajaDets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArqueoCajaDet arqueoCajaDet = db.ArqueoCajaDets.Find(id);
            db.ArqueoCajaDets.Remove(arqueoCajaDet);
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