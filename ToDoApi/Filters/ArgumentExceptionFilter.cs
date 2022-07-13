using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace TodoApiDTO.Filters
{
    public sealed class ArgumentExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException || context.Exception is ArgumentNullException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            base.OnException(context);
        }
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is ArgumentException || context.Exception is ArgumentNullException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            return base.OnExceptionAsync(context);
        }
    }
}
