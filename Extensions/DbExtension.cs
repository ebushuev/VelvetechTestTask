using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApiDTO.DAL;
using TodoApiDTO.Options;

namespace TodoApiDTO.Extensions;

public static class DbExtension
{
    public static void AddCustomDb(this WebApplicationBuilder builder)
    {
        var dbOption = builder.Configuration.GetSection("Postgres").Get<DbOption>()!;
        
        var connectionString =
            $"Host={dbOption.Host};Port={dbOption.Port};Database={dbOption.Database};Username={dbOption.User};Password={dbOption.Password};";

        
        builder.Services.AddDbContext<TodoContext>(options =>
            options.UseNpgsql(connectionString));
    }

    public static void AddMigration(this WebApplication app)
    {
        using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
        
        if (serviceScope == null) return;
        
        var applicationContext = serviceScope.ServiceProvider.GetRequiredService<TodoContext>();
        applicationContext.Database.Migrate();
    }
}