using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Velvetech.Todo.Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args)
  .ConfigureServices(services => services.AddAutofac())
  .Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            })
            .UseSerilog((context, ñ) => ñ.ReadFrom.Configuration(context.Configuration));
  }
}
