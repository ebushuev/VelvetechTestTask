using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;

namespace TodoApi;

public class Program
{
    public static void Main(string[] args)
    {
        /*var logger = LogManager.GetCurrentClassLogger();
        logger.Info("Starting application...");*/
        
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}