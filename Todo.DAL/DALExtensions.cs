using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.DAL.Repositories;
using TodoApi.DAL.Contexts;

namespace Todo.DAL
{
    public static class DALExtensions
    {
        public static void AddDAL(this IServiceCollection services, string conStr)
        {
            services.AddDbContext<TodoContext>(opt =>
               opt.UseSqlServer(conStr));
            // TODO: add some DAL layer services
            services.AddTransient<ITodoRepository, TodoRepository>();
        }
    }
}
