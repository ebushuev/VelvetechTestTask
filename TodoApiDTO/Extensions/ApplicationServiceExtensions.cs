using Application.IServices;
using Application.Services;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TodoApiDTO.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
    {

      services.AddDbContext<TodoContext>(opt =>
               opt.UseSqlServer(config.GetConnectionString("TodoList")));

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoList", Version = "v1" });
      });

      services.AddScoped<ITodoListRepository, TodoListRepository>();

      return services;
    }
  }
}
