using Microsoft.EntityFrameworkCore;
using Velvetech.TodoApp.Domain.Entities;

namespace Velvetech.TodoApp.Infrastructure.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItemEntity> TodoItems { get; set; }
    }
}
