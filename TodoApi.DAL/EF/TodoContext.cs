using Microsoft.EntityFrameworkCore;
using TodoApi.DAL.Entities;

namespace TodoApi.DAL.EF
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}