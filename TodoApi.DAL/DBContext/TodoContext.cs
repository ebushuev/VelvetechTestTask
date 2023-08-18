using Microsoft.EntityFrameworkCore;
using TodoApi.DAL.Entities;

namespace TodoApi.DAL.DBContext
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