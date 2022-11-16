using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System.Net;

namespace TodoApiDTO.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;
            _logger.Error(filterContext.Exception); // пишем в лог
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
