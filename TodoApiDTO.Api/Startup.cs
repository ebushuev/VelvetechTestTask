using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApiDTO.Api.Extensions.CustomExceptionMiddleware;
using TodoApiDTO.Api.Extensions.CustomLogging;
using TodoApiDTO.Api.Validation;
using TodoApiDTO.Core.Models;
using TodoApiDTO.Core.Services;
using TodoApiDTO.EF;
using TodoApiDTO.EF.Services;

namespace TodoApiDTO.Api
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var logFilePath = Configuration["Logging:Target"];

            services.AddDbContext<TodoContext>(options => { options.UseSqlServer(connectionString); });

            services.AddScoped<IModelValidator<TodoItemCreateDTO>, TodoItemCreateDTOValidator>();
            services.AddScoped<IModelValidator<TodoItemDTO>, TodoItemDTOValidator>();
            services.AddScoped<ITodoService, TodoService>();

            services.AddControllers();
            services.AddSwaggerGen();

            services.AddLogging(builder => { builder.AddFile(logFilePath); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(setupAction =>
                {
                    setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApiDTO API");
                    setupAction.RoutePrefix = string.Empty;
                });
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}