using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging.Filters
{
    public class TokenValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // get token
            var token = context.HttpContext.Request.Headers["x-token"];
            if (string.IsNullOrWhiteSpace(token))
            {
                //context.Result = new ContentResult()
                //{
                //    Content = "Resource unavailable - header not set."
                //};
                context.Result = new BadRequestResult();
            }
            if (token != "PUT-YOUR-TOKEN-HERE")
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
