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
    public class IngresoComunidadsController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: IngresoComunidads
        [Authorize]
        public ActionResult Index(string dato)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            var ic = from m in db.IngresoComunidads select m;

            if (!string.IsNullOrEmpty(dato))
            {
                ic = ic.Where(m => m.Comunidad1.nombre.Contains(dato) || m.Kermesse1.nombre.Contains(dato) || m.Producto1.nombre.Contains(dato));
            }

            return View(ic.ToList());
        }

        // GET: IngresoComunidads/Details/5
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
            IngresoComunidad ingresoComunidad = db.IngresoComunidads.Find(id);
            if (ingresoComunidad == null)
            {
                return HttpNotFound();
            }
            return View(ingresoComunidad);
        }

        // GET: IngresoComunidads/Create
        [Authorize]
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre");
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre");
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre");
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName");
            return View();
        }

        // POST: IngresoComunidads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idIngresoComunidad,kermesse,comunidad,producto,cantProducto,totalBonos,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] IngresoComunidad ingresoComunidad)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }


            if (ModelState.IsValid)
            {
                ingresoComunidad.usuarioCreacion = int.Parse(Session["UserID"].ToString());
                ingresoComunidad.fechaCreacion = DateTime.Now;
                db.IngresoComunidads.Add(ingresoComunidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre", ingresoComunidad.comunidad);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", ingresoComunidad.kermesse);
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre", ingresoComunidad.producto);
            ViewBag.usuarioCreacion = new SelectList(db.Usuarios, "idUsuario", "userName", ingresoComunidad.usuarioCreacion);            
            return View(ingresoComunidad);
        }

        // GET: IngresoComunidads/Edit/5
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
            IngresoComunidad ingresoComunidad = db.IngresoComunidads.Find(id);
            if (ingresoComunidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre", ingresoComunidad.comunidad);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", ingresoComunidad.kermesse);
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre", ingresoComunidad.producto);
            return View(ingresoComunidad);
        }

        // POST: IngresoComunidads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idIngresoComunidad,kermesse,comunidad,producto,cantProducto,totalBonos,usuarioCreacion,fechaCreacion,usuarioModificacion,fechaModificacion,usuarioEliminacion,fechaEliminacion")] IngresoComunidad ingresoComunidad)
        {
            if (Session["UserID"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }


            IngresoComunidad i = db.IngresoComunidads.Find(ingresoComunidad.idIngresoComunidad);

            i.kermesse = ingresoComunidad.kermesse;
            i.comunidad = ingresoComunidad.comunidad;
            i.producto = ingresoComunidad.producto;
            i.cantProducto = ingresoComunidad.cantProducto;
            i.totalBonos = ingresoComunidad.totalBonos;
            i.usuarioModificacion = int.Parse(Session["UserID"].ToString());
            
            i.fechaModificacion = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(i).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.comunidad = new SelectList(db.Comunidads, "idComunidad", "nombre", ingresoComunidad.comunidad);
            ViewBag.kermesse = new SelectList(db.Kermesses, "idKermesse", "nombre", ingresoComunidad.kermesse);
            ViewBag.producto = new SelectList(db.Productoes, "idProducto", "nombre", ingresoComunidad.producto);
            
            return View(ingresoComunidad);
        }

        // GET: IngresoComunidads/Delete/5
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
            IngresoComunidad ingresoComunidad = db.IngresoComunidads.Find(id);
            if (ingresoComunidad == null)
            {
                return HttpNotFound();
            }
            return View(ingresoComunidad);
        }

        // POST: IngresoComunidads/Delete/5
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

            IngresoComunidad ingresoComunidad = db.IngresoComunidads.Find(id);
            db.IngresoComunidads.Remove(ingresoComunidad);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptIngresoComunidad.rdlc");
            rpt.ReportPath = ruta;

            List<VwIngresoComunidad> ls = new List<VwIngresoComunidad>();
            var ic = from m in db.VwIngresoComunidads select m;

            if (!string.IsNullOrEmpty(busq))
            {
                ic = ic.Where(m => m.comunidad.Contains(busq) || m.kermesse.Contains(busq) || m.producto.Contains(busq));
            }

            ls = ic.ToList();


            ReportDataSource rd = new ReportDataSource("DSIngresoComunidad", ls);
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

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RptIngresoComunidadVertical.rdlc");
            rpt.ReportPath = ruta;

            VwIngresoComunidad i = db.VwIngresoComunidads.Find(id);
            List<VwIngresoComunidad> ls = new List<VwIngresoComunidad>();
            ls.Add(i);
            
            ReportDataSource rd = new ReportDataSource("DSIngresoComunidad", ls);
            rpt.DataSources.Add(rd);

            var b = rpt.Render("PDF", null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }
    }
}
