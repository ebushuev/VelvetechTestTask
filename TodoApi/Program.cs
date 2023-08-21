namespace TodoApi
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Serilog.Events;
    using TodoApi.Repository;

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            ApplyDatabaseMigrations(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((context, configuration) =>
                    configuration
                        .WriteTo.Console()
                        .WriteTo.File("TodoApi.log", LogEventLevel.Error, fileSizeLimitBytes: 10240, rollOnFileSizeLimit: true),
                preserveStaticLogger: true);

        private static void ApplyDatabaseMigrations(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TodoContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
