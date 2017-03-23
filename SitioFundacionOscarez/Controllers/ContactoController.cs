using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SitioFundacionOscarez.Models;
using SitioFundacionOscarez.Seguridad;

namespace SitioFundacionOscarez.Controllers
{
    [CustomAuthorization]
    public class ContactoController : Controller
    {
        private DbFundacionEntities db = new DbFundacionEntities();
        private Notificaciones ObjNotificar;

        // GET: /Contacto/
        public ActionResult Index()
        {
            return View(db.Contactenos.ToList());
        }

        // GET: /Contacto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactenos contactenos = db.Contactenos.Find(id);
            if (contactenos == null)
            {
                return HttpNotFound();
            }
            return View(contactenos);
        }

        [AllowAnonymous]
        // GET: /Contacto/Create
        public ActionResult Contactanos()
        {
            ViewBag.Envio = false;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contactanos([Bind(Include = "ID,Nombre,email,Telefono,Asunto,Mensaje,FechaSolicitud")] Contactenos contactenos)
        {
            if (ModelState.IsValid)
            {
                contactenos.FechaSolicitud = DateTime.Now;
                db.Contactenos.Add(contactenos);
                db.SaveChanges();
                ObjNotificar = new Notificaciones();
                ObjNotificar.Asunto = contactenos.Asunto;
                ObjNotificar.Mensaje = contactenos.Mensaje;
                ObjNotificar.EnviarNotificacion();
                ViewBag.Envio = true;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewBag.Envio = false;
                return View(contactenos);
            }
        }

        // GET: /Contacto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactenos contactenos = db.Contactenos.Find(id);
            if (contactenos == null)
            {
                return HttpNotFound();
            }
            return View(contactenos);
        }

        // POST: /Contacto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Nombre,email,Telefono,Asunto,Mensaje,FechaSolicitud")] Contactenos contactenos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactenos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactenos);
        }

        // GET: /Contacto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactenos contactenos = db.Contactenos.Find(id);
            if (contactenos == null)
            {
                return HttpNotFound();
            }
            return View(contactenos);
        }

        // POST: /Contacto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contactenos contactenos = db.Contactenos.Find(id);
            db.Contactenos.Remove(contactenos);
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
