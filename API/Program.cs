using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TodoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex) {
                Log.Fatal(ex, "api stopped!");
            }
            finally {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(ConfigureLogger)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });

        private static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config) {
            config
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext();
        }
    }
}
