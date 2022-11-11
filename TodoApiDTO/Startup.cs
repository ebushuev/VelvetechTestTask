using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApiDTO.Extensions;
using TodoApiDTO.Filters;
using TodoApiDTO.ModelValidations.TodoItems;
using FluentValidation.AspNetCore;
using FluentValidation;

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
            services.AddDbContextServices(Configuration);
            services.AddCorsConfigurations();
            services.AddServiceConfigurations();
            services.AddValidatorsFromAssembly(typeof(CreateTodoItemRequestModelValidator).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddControllers(options =>
            {
                options.Filters.Add(new ActionFilter());
            });

            services.AddSwaggerServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.AddSwaggerConfiguration();
                app.UseDeveloperExceptionPage();
            }

            app.UseApiResponseMiddleware();

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.MigrateDatabase();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
