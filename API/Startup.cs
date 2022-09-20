using BLL.Repository;
using DAL.DataContext;
using DAL.IRepository;
using DataSeed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using TodoApi.Extensions;

namespace TodoApi {
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddVTSwagger();
            services.AddAutoMapper();
            services.AddControllers();

            services.AddScoped<ITodoRepository, TodoRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseVTSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            string connection = Configuration.GetConnectionString("DefaultConnection");
            var contextOptions = new DbContextOptionsBuilder<TodoContext>()
                .UseSqlServer(connection).Options;
            using (var context = new TodoContext(contextOptions)) {
                var pendingMigrations = context.Database.GetPendingMigrations();
                if (pendingMigrations.Any()) {
                    context.Database.Migrate();
                }

				DataSeeder.SeedData(context);
            }
        }
    }
}
