using System.Web.Mvc;
using Demo.Common.Attributes;

namespace Demo.Common.ExtendedClass
{
    // Sequence to add filter attributes
    // 1. OnAuthorization
    // 2. OnException
    // 3. OnActionExecuting
    // 4. OnActionExecuted
    // 5. OnResultExecuting
    // 6. OnResultExecuted
    [CustomAuthorize]
    [CustomExceptionFilter]
    [CustomActionFilter]
    [ValidateModel]
    public class BaseController : Controller
    {
    }
}
