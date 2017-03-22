using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace SitioFundacionOscarez.Seguridad
{
    public class CustomAuthorization : AuthorizeAttribute
    {

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] == null)
            {
                filterContext.HttpContext.Session["URLRedirect"] = filterContext.HttpContext.Request.Url.AbsoluteUri;
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Login", action = "Login" })
                );
            }
        }


    }
}