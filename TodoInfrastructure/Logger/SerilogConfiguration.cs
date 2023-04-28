using Microsoft.Extensions.Hosting;
using Serilog;

namespace TodoInfrastructure.Logger
{
    public static class SerilogConfiguration
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder builder)
        {
            var loggerConfig = new LoggerConfiguration()
                .WriteTo.File("Logs/errors-log.logs",
                            rollingInterval: RollingInterval.Day)
                .MinimumLevel.Error()
                .CreateLogger();

            builder.UseSerilog(loggerConfig);
            return builder;
        }
    }
}
