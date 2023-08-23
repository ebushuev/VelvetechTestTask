using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using TodoApi.Models;

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
            services.AddDbContext<TodoContext>(opt =>
               opt.UseInMemoryDatabase("TodoList"));
            services.AddControllers();
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
        }
    }
}
