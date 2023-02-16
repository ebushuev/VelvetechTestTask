using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq.Expressions;

namespace TodoApiDTO.Infrastructure
{
    public class NRExceptionsFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(NullReferenceException))
            {
                context.Result = new NotFoundResult(); 
            }
        }
    }
}
