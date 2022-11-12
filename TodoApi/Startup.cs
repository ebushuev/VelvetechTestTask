using Application;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TodoApiDTO;

namespace TodoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsWepApi()
                .AddAutoMapperWepApi()
                .AddSwaggerGetWepApi()
                .AddApplication()
                .AddDbContextData()
                .AddMSSqlDatabaseData(Configuration);

            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.AddSwaggerUIWebApi();
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.AddErrorHandingMiddlewareWebApi();

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
