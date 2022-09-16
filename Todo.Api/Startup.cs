using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Velvetech.Todo.Api.Middlewares;

namespace Velvetech.Todo.Api
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
      services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(Todo.Logic.Models.TodoItemsMappingProfile)));
      services.AddControllers();
      services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Version = "v1" }));
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
      builder.RegisterModule(new AutofacModule(Configuration));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseMiddleware<ErrorHandlingMiddleware>();
      app.UseSwagger();
      app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "Todo API"));

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
