using System;
using System.Linq;
using System.Web.Helpers;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http;

namespace Demo.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AjaxValidateAntiForgeryTokenAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers;
            var headerToken = headers.Contains("__RequestVerificationToken")
                ? headers.GetValues("__RequestVerificationToken").FirstOrDefault()
                : null;
            var cookieToken = headers.GetCookies().Select(x => x[AntiForgeryConfig.CookieName]).FirstOrDefault();

            if (headerToken == null || cookieToken == null)
                return false;

            try
            {
                AntiForgery.Validate(cookieToken.Value, headerToken);
            }
            catch
            {
                return false;
            }

            return base.IsAuthorized(actionContext);
        }
    }
}
