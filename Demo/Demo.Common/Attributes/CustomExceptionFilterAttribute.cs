using System;
using System.Web;
using System.Web.Mvc;

namespace Demo.Common.Attributes
{
    /// <summary>
    /// This action filter will handle the errors which has http response code 500. 
    /// As Ajax is not handling this error.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CustomExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        private Type _exceptionType = typeof(Exception);

        private const string DefaultView = "Error";

        private const string DefaultAjaxView = "_Error";

        public Type ExceptionType
        {
            get
            {
                return _exceptionType;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _exceptionType = value;
            }
        }

        public string View { get; set; }

        public string Master { get; set; }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.IsChildAction ||
                (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled))
            {
                return;
            }
            var innerException = filterContext.Exception;

            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                // adding the internal server error (500 status http code)
                if ((new HttpException(null, innerException).GetHttpCode() != 500) ||
                    !ExceptionType.IsInstanceOfType(innerException))
                {
                    return;
                }
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                // checking for Ajax request
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var result = new PartialViewResult
                    {
                        ViewName = string.IsNullOrEmpty(View) ? DefaultAjaxView : View,
                        ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                        TempData = filterContext.Controller.TempData
                    };
                    filterContext.Result = result;
                }
                else
                {
                    var result = CreateActionResult(filterContext, model);
                    filterContext.Result = result;
                }

            }
            filterContext.ExceptionHandled = true;
        }

        private ActionResult CreateActionResult(ExceptionContext filterContext, HandleErrorInfo model)
        {
            var result = new ViewResult
            {
                ViewName = string.IsNullOrEmpty(View) ? DefaultView : View,
                MasterName = Master,
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData,
            };

            result.TempData["Exception"] = filterContext.Exception;

            return result;
        }
    }
}