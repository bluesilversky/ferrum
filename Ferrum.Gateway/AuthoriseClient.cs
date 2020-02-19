using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ferrum.Gateway
{
    public class AuthoriseClientAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach(var arg in context.ActionArguments)
                Debug.WriteLine($"{arg.Key} : {arg.Value}");

            await next();
        }
    }
}
