using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoApi.Common.Exceptions;

namespace TodoApi.Server.Midddlewares
{
    public class TodoExceptionFilter : IExceptionFilter
    {

        public TodoExceptionFilter() 
        {
        }

        /// <summary>
        /// Handle DataNotFoundException
        /// </summary>
        /// <param name="context">Exception context</param>
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DataNotFoundException) 
            {
                context.Result = new NotFoundResult();
                context.ExceptionHandled = true;
            }
        }
    }
}
