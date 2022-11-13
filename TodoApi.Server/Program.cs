using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TodoApi.Server.Helpers;

namespace TodoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var migrationHelper = new MigrationHelper();
            migrationHelper.Migrate(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
