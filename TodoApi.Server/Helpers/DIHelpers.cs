using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TodoApi.Core.Services.Contract;
using TodoApi.Core.Services.Implementation;
using TodoApiRepository.Context;
using TodoApiRepository.Repositories.Contract;
using TodoApiRepository.Repositories.Implementation;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System;

namespace TodoApi.Server.Helpers
{
    public static class DIHelpers
    {
        /// <summary>
        /// Add xml comments to swagger
        /// </summary>
        /// <param name="options">Swagger option</param>
        public static void AddSwagerXmlFile(this SwaggerGenOptions options) 
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);
            foreach (var fileInformation in directory.EnumerateFiles("*.xml"))
            {
                options.IncludeXmlComments(fileInformation.FullName);
            }
        } 

        /// <summary>
        /// Add database to DI
        /// </summary>
        /// <param name="serviceCollection">DI service collection</param>
        /// <param name="connectionString"></param>
        public static void AddDatabase(this IServiceCollection serviceCollection, string connectionString) 
        {
            serviceCollection.AddDbContext<TodoContext>(opt => opt.UseSqlServer(connectionString));
            serviceCollection.AddScoped<ITodoItemRepository, TodoItenRepository>();
        }

        /// <summary>
        /// Add services to service collection
        /// </summary>
        /// <param name="serviceCollection">Service collection</param>
        public static void AddServices(this IServiceCollection serviceCollection) 
        {
            serviceCollection.AddScoped<ITodoItemService, TodoItemService>();
        }
    }
}
