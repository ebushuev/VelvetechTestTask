using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using TodoApiDTO.BL.Interfaces;
using TodoApiDTO.BL.Service;
using TodoApiDTO.DAL.Data;
using TodoApiDTO.Models;

namespace TodoApiDTO
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddDbContext<TodoContext>( opt =>
               opt.UseSqlServer( Configuration.GetConnectionString( "DbConnection" ) ) );

            services.AddControllers();

            services.AddScoped<ITodoItemService, TodoItemService>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.File( "logs\\log-.txt", rollingInterval: RollingInterval.Day )
                .CreateLogger();

            services.AddSwaggerGen( c => c.SwaggerDoc( "v1",
                new OpenApiInfo() 
                { 
                    Title = "TodoItems", 
                    Version = "v1",
                    Description = "Web API"
                } ) );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if( env.IsDevelopment() ) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI( c => c.SwaggerEndpoint( "/swagger/v1/swagger.json", "TodoItems v1" ) );

            

            app.UseExceptionHandler( errorApp =>
            {
                errorApp.Run( async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    Log.Error( exceptionHandlerPathFeature.Error, "Unhandled exception occurred" );

                    context.Response.StatusCode = 500;

                    await context.Response.WriteAsync( "An unexpected fault happened. Try again later." );
                } );
            } );

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
            {
                endpoints.MapControllers();
            } );
        }
    }
}
