using System.Web.Mvc;
using System.Web.Routing;

namespace Demo.Common.Attributes
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;

            // check if session is supported
            if (httpContext.Session != null)
            {
                // check if a new session id was generated  
                if (httpContext.Session.IsNewSession)
                {
                    // If you have created a new session , but there are old cookies that means is that we have a timeout
                    var sessionCookie = httpContext.Request.Headers["Cookie"];
                    if ((sessionCookie != null) &&
                        (sessionCookie.IndexOf("ASP.NET_SessionId", System.StringComparison.Ordinal) >= 0))
                    {
                        var redirectTarget = new RouteValueDictionary { { "action", "Error" }, { "controller", "Error" } };
                        filterContext.Result = new RedirectToRouteResult(redirectTarget);
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}