using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Todo.Core.Interfaces;
using Todo.Core.Mappings;
using Todo.Core.Services;
using Todo.Infrastructure.DbContexts;
using Todo.Infrastructure.Entities;
using Todo.Infrastructure.Interfaces;
using Todo.Infrastructure.Repositories;

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
            services.AddTransient<IRepository<TodoItem>, Repository<TodoItem>>();
            services.AddTransient<IItemService, ItemService>();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDbContext<TodoDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Sql")));

            services.AddControllers();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // You can define additional routes here
                endpoints.MapControllerRoute(
                    name: "TodoItemById",
                    pattern: "api/todo/{todoItemId}", // Define your custom route pattern here
                    defaults: new { controller = "TodoItems", action = "GetTodoItem" }); // Controller and action names
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1"); });
        }
    }
}