using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Models;

namespace TodoApi.Infrastructure.DataAccess
{
    public class TodoDbContext : DbContext, ITodoDbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
            //TODO: Configure migrations
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}