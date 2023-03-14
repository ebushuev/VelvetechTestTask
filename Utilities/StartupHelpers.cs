using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace TodoApiDTO
{
    public static class StartupHelpers
    {
        public static void InitSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TODO List - API для управления списком задач",
                    Description = "Тестовое задание (вариант №1) для Velvetech by Шариков Роман a.k.a. R0m43ss",
                });
            });

        }

        public static void InitLogging(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder => {
                loggingBuilder.AddFile("!log\\{0:yyyy}-{0:MM}-{0:dd}.log", fileLoggerOptions => {
                    fileLoggerOptions.FormatLogFileName = fileName => {
                        return string.Format(fileName, DateTime.Now);
                    };
                    fileLoggerOptions.FilterLogEntry = (logMessage) => {
                        return logMessage.LogLevel == LogLevel.Error;
                    };
                    fileLoggerOptions.FormatLogEntry = (logMessage) =>
                    {
                        var sb = new StringBuilder();
                        sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));
                        sb.Append($"{Enum.GetName(typeof(LogLevel), logMessage.LogLevel).ToUpper()} ");
                        sb.Append(logMessage.Message);
                        return sb.ToString();
                    };
                });
            });
        }
    }
}
