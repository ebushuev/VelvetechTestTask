using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApiDTO.DAL
{
    public class TodoContext : DbContext
    {

        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

    }
}