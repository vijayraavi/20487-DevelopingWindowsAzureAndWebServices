using Microsoft.AspNet.Mvc.Filters;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mod03.Attributes
{
    public class DebugFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("DEBUG pre-processing:");
            Debug.WriteLine("Processing request: " + context.HttpContext.Request.Path.ToUriComponent());
            Debug.WriteLine("Will be handled by the following controller: " + context.Controller.GetType().FullName);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("DEBUG post-processing:");

            Debug.WriteLine("Processed action: " + context.ActionDescriptor.DisplayName);
            Debug.WriteLine("Response status code: " + context.HttpContext.Response.StatusCode.ToString());
        }
    }
}
