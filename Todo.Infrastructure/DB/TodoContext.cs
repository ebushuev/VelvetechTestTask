using Microsoft.EntityFrameworkCore;

namespace Todo.Infrastructure.DB
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItem { get; set; }
    }
}
