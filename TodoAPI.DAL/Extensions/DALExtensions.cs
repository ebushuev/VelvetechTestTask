using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.DAL.Repositories;
using TodoApi.DAL.Context;
using TodoApi.DAL.Interfaces;
using TodoApi.DAL.Models;

namespace TodoApi.DAL
{
    public static class DALExtensions
    {
        public static void AddDAL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TodoContext>(opt =>
               opt.UseSqlServer(connectionString));
            services.AddTransient<IRepository<TodoItem>, TodoItemService>();
        }
    }
}