using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.DataAccess.Data;
using Todo.DataAccess.Repository;
using Todo.DataAccess.Repository.IRepository;

namespace Todo.DataAccess.Extensions
{
    public static class DataAccessExtension
    {
        public static void AddDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(connectionString));

            services.AddTransient<ITodoItemRepository, TodoItemRepository>();
        }
    }
}