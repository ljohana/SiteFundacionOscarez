using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SitioFundacionOscarez.Seguridad;

namespace SitioFundacionOscarez.Controllers
{
    [CustomAuthorization]
    public class ConvocatoriasController : Controller
    {
        [AllowAnonymous]
        // GET: /Convocatorias/
        public ActionResult Convocatorias()
        {
            return View();
        }
	}
}