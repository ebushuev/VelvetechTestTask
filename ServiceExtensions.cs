using  Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using TodoApi.Models;
using System.IO;
using System.Text.Json;

namespace TodoApiDTO
{
    public static class ServiceExtensions
    {
        public static Serilog.ILogger BuildSerilogLogger(this IWebHostEnvironment env)
        {
            var appAssembly = typeof(ServiceExtensions).Assembly;
            var currentDirectory = Path.GetDirectoryName(appAssembly.Location);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("serilogsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables("ToDo")
                .Build();

               Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return Log.Logger;
        }

        public static void LogRestApiCallStart(this Microsoft.Extensions.Logging.ILogger logger, object request, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            logger.LogDebug("Call ApiCall method {0}\n {1}", memberName, JsonSerializer.Serialize(request));
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"];
            services.AddDbContext<TodoContext>(o => o.UseSqlServer(connectionString));
        }
    }
}