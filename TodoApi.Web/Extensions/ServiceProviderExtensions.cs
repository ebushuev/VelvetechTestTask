using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using TodoApi.Application.Queries.GetTodoItemList;
using TodoApi.Application.Shared;
using TodoApi.Domain.BusinessRules;
using TodoApi.Infrastructure.DataAccess;

namespace TodoApi.Web.Extensions
{
    public static class ServiceProviderExtensions
    {

        public static void ConfigureDataBase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TodoDbContext>(opt =>
                opt.UseSqlServer(connectionString));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoDbContext, TodoDbContext>();
            services.AddTransient<ILogger>(_ => LogManager.GetCurrentClassLogger());

            services.AddTransient<ITodoItemBusinessRules, TodoItemBusinessRules>();

            services.AddAutoMapper(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            services.AddMediatR(typeof(GetTodoItemListQuery).Assembly);
        }
    }
}
