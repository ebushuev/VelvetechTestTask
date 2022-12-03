using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using TodoApiDTO.Components.TodoList.DbContexts;
using TodoApiDTO.Components.TodoList.Interfaces;
using TodoApiDTO.Components.TodoList.Services;
using TodoApiDTO.Extensions;

namespace TodoApiDTO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.Setup()
                .LoadConfiguration(builder =>
                {
                    builder.ForLogger()
                        .FilterMinLevel(LogLevel.Info)
                        .WriteToConsole();

                    builder.ForLogger()
                        .FilterMinLevel(LogLevel.Info)
                        .WriteToFile(fileName: "ERRORS_${shortdate}.log");
                });

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt =>
               opt.UseInMemoryDatabase("TodoList"));

            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<TodoCrudService, TodoCrudService>();

            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API");

                    // Opens swagger UI as a default page.
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.AddGlobalErrorHandler();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}