using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StajProject.Filters
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            var URL = filterContext.HttpContext.Request.Url.OriginalString;
            if ( session["Email"] != null && session["Password"] != null)
                return;

            //Redirect him to somewhere.
            var redirectTarget = new RouteValueDictionary
             {{"action", "Login"}, {"controller", "Home"}, {"previousURL", URL }};
            filterContext.Result = new RedirectToRouteResult(redirectTarget);
        }
    }
}