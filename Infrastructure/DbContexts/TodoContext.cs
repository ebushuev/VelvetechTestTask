using Microsoft.EntityFrameworkCore;
using TodoApiDTO.Application.ToDo;
namespace TodoApiDTO.Infrastructure.DbContexts
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}