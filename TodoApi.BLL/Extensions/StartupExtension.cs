using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.BLL.Extensions.MappingProfiles;
using TodoApi.BLL.Services;
using TodoApi.BLL.Services.Contracts;

namespace TodoApi.BLL.Extensions;

public static class StartupExtension
{
    public static void AddBLLServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IServiceTodoItem, ServiceTodoItem>();
        
        services.AddAutoMapper(typeof(TodoItemMappingProfile));
    }
}