using Application.IServices;
using Application.Services;
using DataAccessLayer.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TodoApiDTO.Behaviors;
using TodoApiDTO.Services;

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
      services.AddSingleton<ICurrentUserService, CurrentUserService>();
      
      services.AddHttpContextAccessor();

      services.AddTransient(typeof(IPipelineBehavior<,>),
          typeof(LoggingBehavior<,>));

      return services;
    }
  }
}
