using Microsoft.EntityFrameworkCore;
using TodoApi.DAL.Models;

namespace TodoApi.DAL
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