using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Todo.BLL.Interfaces;
using Todo.BLL.Services;
using Todo.DAL;
using Todo.DAL.DbContexts;
using Todo.DAL.Entities;

namespace Todo.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureRepositories(this IServiceCollection services) =>
        services.AddScoped<IRepository<TodoItem>, Repository<TodoItem>>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<TodoDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

    public static void ConfigureServices(this IServiceCollection services) =>
        services.AddScoped<ITodoItemService, TodoItemService>();

    public static void ConfigureSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo", Version = "v1" });
        });
}
