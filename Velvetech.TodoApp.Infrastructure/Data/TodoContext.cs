using Microsoft.EntityFrameworkCore;
using Velvetech.TodoApp.Domain.Entities;

namespace Velvetech.TodoApp.Infrastructure.Data
{
    public class TodoContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable property 'TodoItems' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public TodoContext(DbContextOptions<TodoContext> options)
#pragma warning restore CS8618 // Non-nullable property 'TodoItems' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
            : base(options)
        {
        }

        public DbSet<TodoItemEntity> TodoItems { get; set; }
    }
}
