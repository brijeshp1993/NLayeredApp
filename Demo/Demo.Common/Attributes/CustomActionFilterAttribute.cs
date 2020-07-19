using System.Web.Mvc;

namespace Demo.Common.Attributes
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // ... log stuff before execution            
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // ... log stuff after execution
        }
    }
}
