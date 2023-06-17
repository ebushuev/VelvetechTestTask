using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Todo.BL.DTOs;
using Todo.DAL.DbContexts;

namespace Todo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();

            var enableInMemoryDb = Configuration.GetSection("SqlServer").GetValue<bool>("UseInMemoryDatabase");
            if (enableInMemoryDb)
            {
                services.AddDbContext<TodoContext>(opt => {
                    opt.UseInMemoryDatabase("TodoList");
                });
            }
            else
            {
                services.AddDbContext<TodoContext>(opt =>
                {
                    var connectionString = Configuration.GetConnectionString("DbConnection");
                    opt.UseSqlServer(connectionString);
                });
                // services.BuildServiceProvider().GetService<TodoContext>().Database.Migrate();
            }
            
            services.AddControllers();

            services.AddMediatR(typeof(TodoItemDTO));
            services.AddAutoMapper(typeof(TodoItemDTO));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
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
