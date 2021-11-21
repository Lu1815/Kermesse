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
    public class ArqueoCajasController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: ArqueoCajas
        [Authorize]
        public ActionResult Index(string dato)
        {
            var arqueoCajas = from m in db.ArqueoCajas select m;

            if (!string.IsNullOrEmpty(dato))
            {
                arqueoCajas = arqueoCajas.Where(m => m.fechaArqueo.ToString().Contains(dato) || m.Kermesse1.nombre.Contains(dato) || m.granTotal.ToString().Contains(dato));
            }

            return View(arqueoCajas.ToList());
        }

        // GET: ArqueoCajas/Details/5
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
            ArqueoCaja arqueoCaja = db.ArqueoCajas.Find(id);

            if (arqueoCaja == null)
            {
                return HttpNotFound();
            }
            return View(arqueoCaja);
        }

        // GET: ArqueoCajas/Create
        [Authorize]
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre");
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            return View();
        }

        // POST: ArqueoCajas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idArqueoCaja,kermesse,fechaArqueo,granTotal,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] ArqueoCaja arqueoCaja)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            arqueoCaja.usuarioCreacion = int.Parse(Session["UserID"].ToString(), System.Globalization.NumberStyles.Integer);
            arqueoCaja.fechaCreacion = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                db.ArqueoCajas.Add(arqueoCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", arqueoCaja.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioCreacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioModificacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioEliminacion);
            return View(arqueoCaja);
        }

        // GET: ArqueoCajas/Edit/5}
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
            ArqueoCaja arqueoCaja = db.ArqueoCajas.Find(id);

            if (arqueoCaja == null)
            {
                return HttpNotFound();
            }
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", arqueoCaja.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioCreacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioModificacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioEliminacion);
            return View(arqueoCaja);
        }

        // POST: ArqueoCajas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idArqueoCaja,kermesse,fechaArqueo,granTotal,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] ArqueoCaja arqueoCaja)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(arqueoCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", arqueoCaja.kermesse);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioCreacion);
            ViewBag.usuarioModificacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioModificacion);
            ViewBag.usuarioEliminacion = new SelectList(db.Usuarios, "idUsuario", "userName", arqueoCaja.usuarioEliminacion);
            return View(arqueoCaja);
        }

        // GET: ArqueoCajas/Delete/5
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
            ArqueoCaja arqueoCaja = db.ArqueoCajas.Find(id);
            if (arqueoCaja == null)
            {
                return HttpNotFound();
            }
            return View(arqueoCaja);
        }

        // POST: ArqueoCajas/Delete/5
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

            ArqueoCaja arqueoCaja = db.ArqueoCajas.Find(id);
            db.ArqueoCajas.Remove(arqueoCaja);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptArqueoCaja.rdlc");
            rpt.ReportPath = ruta;

            List<VwArqueoCaja> ls = new List<VwArqueoCaja>();
           

            var arqueoCajas = from m in db.VwArqueoCajas select m;

            if (!string.IsNullOrEmpty(busq))
            {
                arqueoCajas = arqueoCajas.Where(m => m.fechaArqueo.ToString().Contains(busq) || m.kermesse.Contains(busq) || m.granTotal.ToString().Contains(busq));
            }

            ls = arqueoCajas.ToList();


            ReportDataSource rd = new ReportDataSource("DSArqueoCaja", ls);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptArqueoCajaVertical.rdlc");
            rpt.ReportPath = ruta;

            VwArqueoCaja g = db.VwArqueoCajas.Find(id);
            List<VwArqueoCaja> ls = new List<VwArqueoCaja>();
            ls.Add(g);

            ReportDataSource rd = new ReportDataSource("DSArqueoCaja", ls);
            rpt.DataSources.Add(rd);

            var b = rpt.Render("PDF", null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }






    }
}
