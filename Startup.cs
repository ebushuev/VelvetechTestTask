using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TodoApiDTO.BusinessLayer;
using TodoApiDTO.Core.DataAccess;
using TodoApiDTO.Core.Services;
using TodoApiDTO.DataAccessLayer;
using TodoApiDTO.Middlewares;


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
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddDbContext<TodoDbContext>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            //services.AddDbContext<TodoContext>(opt =>
            //  opt.UseInMemoryDatabase("TodoList"));

            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc("todoApiV1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Todo",
                    Description = "Todo Description",
                    Contact = new OpenApiContact
                    {
                         Name = "Ismayil",
                         Email = "xxxxx@gmail.com"
                    }
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseLogException();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/todoApiV1/swagger.json", "Todo API");
                options.RoutePrefix = string.Empty;
            });

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
