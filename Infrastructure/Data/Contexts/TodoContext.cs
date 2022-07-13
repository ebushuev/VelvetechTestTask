using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Data.Models;

namespace TodoApi.Infrastructure.Data.Contexts
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
