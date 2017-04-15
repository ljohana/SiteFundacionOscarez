using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SitioFundacionOscarez.Models;

namespace SitioFundacionOscarez.Controllers
{
    public class HomeController : Controller
    {
        private DbFundacionEntities db = new DbFundacionEntities();
        public ActionResult Index()
        {
            ViewBag.IsHome = true;
            if (db.Database.Exists())
            {
                var proyectos = (from p in db.Proyectos select p).Take(6).ToList();
                ViewBag.Proyectos = proyectos;
                var rand = new Random();
                int l = proyectos.Count();
                int num = rand.Next(l);
                ViewBag.ProyectoAzar = proyectos[num];
                return View();
            }
            else
            {
                ViewBag.Error = "Se ha generado un error de conexión, por favor intentelo de nuevo en unos minutos.";
                return View();
            }
            
        }

        public ActionResult QuienesSomos()
        {
            return View();
        }

        public ActionResult NuestraExperiencia()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}