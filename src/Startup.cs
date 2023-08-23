using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Services;
using TodoApi.Services.Interfaces;

namespace TodoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddControllers();
            services.AddScoped(typeof(IRepository<>), typeof(ItemsRepository<>));
            services.AddScoped<ITodoService, TodoService>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Swagger TodoApi Documentation",
                        Version = "v1",
                        Description = "API Documentation for the TodoApi application",
                        Contact = new OpenApiContact
                        {
                            Name = "Vasilii Mukhin",
                            Email = "vasjenm@gmail.com"
                        },
                        Extensions = new Dictionary<string, IOpenApiExtension>
                        {
                          {"x-logo", new OpenApiObject
                            {
                               {"url", new OpenApiString("~/logo.png")},
                               { "altText", new OpenApiString("TodoApi logo")}
                            }
                          }
                        }

                    });
                options.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => 
                    options.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Swagger TodoApi Documentation v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    System.Console.WriteLine("Migrating database");
                    context.Database.Migrate();
                }
            };
        }
    }
}
