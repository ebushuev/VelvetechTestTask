using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.DAL.DataContext;
using TodoApi.DAL.Models;
using TodoApi.DAL.Repositories;
using TodoApi.DAL.Repositories.Contracts;

namespace TodoApi.DAL;

public static class StartupExtension
{
    public static void AddDALServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TodoApiContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(nameof(TodoApiContext)));
        });
        
        services.AddScoped<IRepository<TodoItemEntity>, Repository<TodoApiContext, TodoItemEntity>>();
    }
}