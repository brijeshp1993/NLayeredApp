using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Demo.Common.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }

            // Check for authorization
            if (HttpContext.Current.Session["UserName"] == null)
            {
                filterContext.Result = filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            var request = httpContext.Request;
            var response = httpContext.Response;
            var user = httpContext.User;

            if (request.IsAjaxRequest())
            {
                // if user is unauthorized Error Code 401
                if (user.Identity.IsAuthenticated == false)
                {
                    response.StatusCode = (int) HttpStatusCode.Unauthorized;
                }
                // if user has insufficient rights Error Code 403
                else if (user.Identity.IsAuthenticated == false)
                {
                    response.StatusCode = (int) HttpStatusCode.Forbidden;
                }

                response.SuppressFormsAuthenticationRedirect = true;
                response.End();
            }

            base.HandleUnauthorizedRequest(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                // The user is not authenticated
                return false;
            }
            // 1. User validation
            var user = httpContext.User;
            if (user.IsInRole("Admin"))
            {
                // Administrator => let him in
                return true;
            }

            // 2.Parameter validation
            var rd = httpContext.Request.RequestContext.RouteData;
            var id = rd.Values["id"] as string;
            if (string.IsNullOrEmpty(id))
            {
                // No id was specified => we do not allow access
                return false;
            }

            // 3.Header validatioin
            var key = httpContext.Request.Headers.GetValues("token");
            // check header token is valid or not bt decrypting it

            return IsAuthorized(user.Identity.Name, id);
        }

        private static bool IsAuthorized(string username, string postId)
        {
            // TODO: you know what to do here
            throw new NotImplementedException();
        }
    }
}
