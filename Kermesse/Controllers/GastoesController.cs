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
    public class GastoesController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: Gastoes
        [Authorize]
        public ActionResult Index(string dato)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            var gasto = from m in db.Gastoes select m;

            if (!string.IsNullOrEmpty(dato))
            {
                gasto = gasto.Where(m => m.concepto.Contains(dato) || m.monto.ToString().Contains(dato) || m.Kermesse1.nombre.Contains(dato) || m.CategoriaGasto.nombreCategoria.Contains(dato) || m.fechGasto.ToString().Contains(dato) || m.monto.ToString().Contains(dato));
            }

            return View(gasto.ToList());
        }

        // GET: Gastoes/Details/5
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
            Gasto gasto = db.Gastoes.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            return View(gasto);
        }

        // GET: Gastoes/Create
        [Authorize]
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            ViewBag.catGasto = new SelectList(db.CategoriaGastoes, "idCatGasto", "nombreCategoria");
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre");
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            return View();
        }

        // POST: Gastoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idGasto,kermesse,catGasto,fechGasto,concepto,monto,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] Gasto gasto)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }


            gasto.usuarioCreacion = int.Parse(Session["UserID"].ToString(), System.Globalization.NumberStyles.Integer);
            gasto.fechaCreacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Gastoes.Add(gasto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.catGasto = new SelectList(db.CategoriaGastoes, "idCatGasto", "nombreCategoria", gasto.catGasto);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", gasto.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioCreacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioEliminacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioModificacion);
            return View(gasto);
        }

        // GET: Gastoes/Edit/5
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
            Gasto gasto = db.Gastoes.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            ViewBag.catGasto = new SelectList(db.CategoriaGastoes, "idCatGasto", "nombreCategoria", gasto.catGasto);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", gasto.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioCreacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioEliminacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioModificacion);
            return View(gasto);
        }

        // POST: Gastoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idGasto,kermesse,catGasto,fechGasto,concepto,monto,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] Gasto gasto)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(gasto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.catGasto = new SelectList(db.CategoriaGastoes, "idCatGasto", "nombreCategoria", gasto.catGasto);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", gasto.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioCreacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioEliminacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", gasto.usuarioModificacion);
            return View(gasto);
        }

        // GET: Gastoes/Delete/5
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
            Gasto gasto = db.Gastoes.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            return View(gasto);
        }

        // POST: Gastoes/Delete/5
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

            Gasto gasto = db.Gastoes.Find(id);
            db.Gastoes.Remove(gasto);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptGastos.rdlc");
            rpt.ReportPath = ruta;

            List<VwGasto> ls = new List<VwGasto>();

            var gasto = from m in db.VwGastoes select m;

            if (!string.IsNullOrEmpty(busq))
            {
                gasto = gasto.Where(m => m.concepto.Contains(busq) || m.monto.ToString().Contains(busq) || m.catGasto.Contains(busq) || m.kermesse.Contains(busq) || m.fechGasto.ToString().Contains(busq) || m.monto.ToString().Contains(busq));
            }

            ls = gasto.ToList();

            ReportDataSource rd = new ReportDataSource("DSGastos", ls);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptGastosVertical.rdlc");
            rpt.ReportPath = ruta;

            VwGasto g = db.VwGastoes.Find(id);
            List<VwGasto> ls = new List<VwGasto>();
            ls.Add(g);

            ReportDataSource rd = new ReportDataSource("DSGasto", ls);
            rpt.DataSources.Add(rd);

            var b = rpt.Render("PDF", null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }

    }


}




