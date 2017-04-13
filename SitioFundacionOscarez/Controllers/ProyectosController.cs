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
    public class ProyectosController : Controller
    {
        private DbFundacionEntities db = new DbFundacionEntities();

        [AllowAnonymous]
        //GET
        public ActionResult ProyectosEstudiantes()
        {
            if (db.Database.Exists())
            {
                var CodSeccion = (from s in db.Secciones where s.Seccion.ToUpper() == "PROGRAMAS PARA ESTUDIANTES" select s.id).FirstOrDefault();
                ViewBag.Proyectos = (from p in db.Proyectos where p.CodSeccion == CodSeccion select p).ToList();
                return View();
            }
            else
            {
                ViewBag.Error = "Se ha generado un error de conexión con la base de datos, por favor intentelo de nuevo en unos minutos.";
                return View();
            }
        }

        // GET: /Proyectos/
        public ActionResult Index()
        {
            return View(db.Proyectos.ToList());
        }

        // GET: /Proyectos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyectos proyectos = db.Proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            return View(proyectos);
        }

        // GET: /Proyectos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Proyectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,CodSeccion,Titulo,Subtitulo,Descripcion,ImagenProyecto,FechaCreacion,FechaModificacion")] Proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                db.Proyectos.Add(proyectos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proyectos);
        }

        // GET: /Proyectos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyectos proyectos = db.Proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            return View(proyectos);
        }

        // POST: /Proyectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,CodSeccion,Titulo,Subtitulo,Descripcion,ImagenProyecto,FechaCreacion,FechaModificacion")] Proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyectos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proyectos);
        }

        // GET: /Proyectos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyectos proyectos = db.Proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            return View(proyectos);
        }

        // POST: /Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proyectos proyectos = db.Proyectos.Find(id);
            db.Proyectos.Remove(proyectos);
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
