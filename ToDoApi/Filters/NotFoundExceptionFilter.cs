using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using TodoApi.Services.Exceptions;

namespace TodoApiDTO.Filters
{
    public sealed class NotFoundExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            base.OnException(context);
        }
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            return base.OnExceptionAsync(context);
        }
    }
}
