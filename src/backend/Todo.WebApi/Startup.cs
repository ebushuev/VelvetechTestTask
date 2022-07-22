using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Todo.DataAccess.Extensions;
using Todo.Services.Extensions;

namespace Todo.WebApi
{
    public class Startup
    {
        private readonly string connectionString;
        public IConfiguration configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = this.configuration.GetConnectionString("DefaultConnection");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataAccess(connectionString);
            services.AddTodoService();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Todo API",
                    Description = "Test task for VelveTech.com",
                    Contact = new OpenApiContact
                    {
                        Name = "Abzal.Tankibayev",
                        Email = "abzal.tankibayev@outlook.com",
                    },
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "PlaceInfo Services"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
