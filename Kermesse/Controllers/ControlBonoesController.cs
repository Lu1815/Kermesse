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
    public class ControlBonoesController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: ControlBonoes
        [Authorize]
        public ActionResult Index()
        {
            return View(db.ControlBonoes.ToList());
        }

        // GET: ControlBonoes/Details/5
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
            ControlBono controlBono = db.ControlBonoes.Find(id);
            if (controlBono == null)
            {
                return HttpNotFound();
            }
            return View(controlBono);
        }

        // GET: ControlBonoes/Create
        [Authorize]
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            Console.WriteLine("HELLO PUTO");
            return View();
        }

        // POST: ControlBonoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idBono,nombre,valor,estado")] ControlBono controlBono)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            Console.WriteLine(controlBono);
            Console.WriteLine("HELLO PUTO");
            if (ModelState.IsValid)
            {
                db.ControlBonoes.Add(controlBono);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(controlBono);
        }

        // GET: ControlBonoes/Edit/5
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
            ControlBono controlBono = db.ControlBonoes.Find(id);
            if (controlBono == null)
            {
                return HttpNotFound();
            }
            return View(controlBono);
        }

        // POST: ControlBonoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idBono,nombre,valor,estado")] ControlBono controlBono)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(controlBono).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(controlBono);
        }

        // GET: ControlBonoes/Delete/5
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
            ControlBono controlBono = db.ControlBonoes.Find(id);
            if (controlBono == null)
            {
                return HttpNotFound();
            }
            return View(controlBono);
        }

        // POST: ControlBonoes/Delete/5
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

            ControlBono controlBono = db.ControlBonoes.Find(id);
            db.ControlBonoes.Remove(controlBono);
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
        public ActionResult verReporte(string tipo)
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptBonos.rdlc");
            rpt.ReportPath = ruta;

            List<ControlBono> ls = new List<ControlBono>();
            ls = db.ControlBonoes.ToList();

            ReportDataSource rd = new ReportDataSource("DSBonos", ls);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptBonosVertical.rdlc");
            rpt.ReportPath = ruta;

            ControlBono g = db.ControlBonoes.Find(id);
            List<ControlBono> ls = new List<ControlBono>();
            ls.Add(g);

            ReportDataSource rd = new ReportDataSource("DSBono", ls);
            rpt.DataSources.Add(rd);

            var b = rpt.Render("PDF", null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }



    }
}
