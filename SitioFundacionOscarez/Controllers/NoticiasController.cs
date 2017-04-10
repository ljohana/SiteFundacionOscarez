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
using System.IO;

namespace SitioFundacionOscarez.Controllers
{
    [CustomAuthorization]
    public class NoticiasController : Controller
    {
        private DbFundacionEntities db = new DbFundacionEntities();

        // GET: /Noticias/
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (db.Database.Exists())
            {
                ViewBag.Error = null;
                return View(db.Noticias.ToList());
            }
            else
            {
                ViewBag.Error = "Se presento un error al intentar cargar las noticias, por favor intentelo de nuevo en unos minutos.";
                return View();
            }
        }

        // GET: /Noticias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
            {
                return HttpNotFound();
            }
            return View(noticias);
        }

        // GET: /Noticias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Noticias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Titulo,Intro,Descripcion,Slide,FechaCreacion")] Noticias noticias, HttpPostedFileBase file)
        {
            try
            {
                var path = Path.Combine(Server.MapPath("~/img/noticias/"), file.FileName);
                file.SaveAs(path);
                noticias.Slide = file.FileName;
                db.Noticias.Add(noticias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(noticias);
            }
        }

        // GET: /Noticias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
            {
                return HttpNotFound();
            }
            return View(noticias);
        }

        // POST: /Noticias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Titulo,Intro,Descripcion,Slide,FechaCreacion")] Noticias noticias, HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    var path = Path.Combine(Server.MapPath("~/img/noticias/"), file.FileName);
                    file.SaveAs(path);
                    noticias.Slide = file.FileName;
                    db.Entry(noticias).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(noticias);
            }
        }

        // GET: /Noticias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
            {
                return HttpNotFound();
            }
            return View(noticias);
        }

        // POST: /Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Noticias noticias = db.Noticias.Find(id);
            db.Noticias.Remove(noticias);
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
