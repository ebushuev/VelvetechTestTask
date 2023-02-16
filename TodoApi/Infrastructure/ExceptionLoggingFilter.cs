using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TodoApiDTO.Infrastructure
{
    public class ExceptionLoggingFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            using (StreamWriter writer = new StreamWriter("exceptions_log.log", true))
            {
                writer.WriteLineAsync($"{DateTime.Now} {context.Exception.Message}");
            }
        }
    }
}
