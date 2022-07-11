using System;
using System.Threading.Tasks;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApiDTO
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<TodoContext>();

            await context.Database.MigrateAsync();
        }
    }
}

