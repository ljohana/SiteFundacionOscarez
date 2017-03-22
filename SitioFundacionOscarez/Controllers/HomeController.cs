using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitioFundacionOscarez.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.IsHome = true;
            return View();
        }

        public ActionResult QuienesSomos()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}