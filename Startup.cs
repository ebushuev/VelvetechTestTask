using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using TodoApi.Models;
using TodoApiDTO.BL;
using TodoApiDTO.BL.Interfaces;
using TodoApiDTO.DAL;
using TodoApiDTO.DAL.Interfaces;
using TodoApiDTO.DAL.Repositories;

namespace TodoApi
{
    public class Startup {
        public Startup( IConfiguration configuration ) {
            Configuration = configuration;
            version = GetType ().Assembly.GetName ().Version.ToString ();
        }

        public IConfiguration Configuration { get; }
        private readonly string version;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services ) {
            services.AddSwaggerGen ( options => {
                options.SwaggerDoc ( version, new OpenApiInfo {
                    Version = $"{version} by GermanBelykh",
                    Title = "ToDoApi",
                    Description = "This project is the result of a task from VelvetechTestTask",
                    Contact = new OpenApiContact {
                        Name = "German Belykh",
                        Email = "germanshpiker@gmail.com",
                        Url = new Uri ( "https://github.com/BelykhGerman" ),
                    }
                } );
                var xmlFilename = $"{Assembly.GetExecutingAssembly ().GetName ().Name}.xml";
                options.IncludeXmlComments ( Path.Combine ( AppContext.BaseDirectory, xmlFilename ) );
            } );
            string demoAppConnection = Configuration.GetConnectionString ( "SQLConnection" ) ?? throw new ArgumentNullException ( nameof ( demoAppConnection ) );
            services.AddDbContextPool<TodoContext> ( options => options.UseSqlServer ( demoAppConnection ) );
            services.AddScoped<IRepository<TodoItem>, TodoItemRepository> ();
            services.AddScoped<IUnitOfWork, EFUnitOfWork> ();
            services.AddScoped<IService<TodoItemDTO>, ServiceTodoItemDTO> ();
            services.AddControllers ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env ) {
            if(env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            else {
                app.UseExceptionHandler ( "/Error" );
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints ( endpoints => {
                endpoints.MapControllers ();
            } );
            app.UseSwagger ();
            app.UseSwaggerUI ( c => {
                c.SwaggerEndpoint ( $"/swagger/{version}/swagger.json", $"Showing API for v{version}" );
            } );
        }
    }
}
