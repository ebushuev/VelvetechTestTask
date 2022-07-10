using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApi.Data;
using TodoApi.Data.Interfaces;
using TodoApi.Services.Services;
using TodoApi.Services.Services.Interfaces;
using TodoApiDTO.Extension;

namespace TodoApiDTO
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
               opt.UseSqlServer(Configuration.GetConnectionString("dbConnection")));

            services.AddControllers(options =>
            {
                options.Filters.Add<ArgumentExceptionFilter>();
            });

            // Register the Swagger services
            RegisterSwaggerService(services);

            // Register internal services
            RegisterInternalServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }

        /// <summary>
        /// All internal services should be register here
        /// </summary>
        /// <param name="services">other services</param>
        private void RegisterInternalServices(IServiceCollection services)
        {
            // Register internal api services
            services.AddTransient<ITodoItemService, TodoItemService>();
            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddSingleton<ITodoItemMappingService, TodoItemMappingService>();
        }

        private void RegisterSwaggerService(IServiceCollection services)
        {
            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.Title = "TodoApi";
                config.Description = "Test api that works with Todo Items.";
            });
        }
    }
}
