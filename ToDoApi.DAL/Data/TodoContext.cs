using Microsoft.EntityFrameworkCore;
using TodoApi.DAL.Models;

namespace TodoApi.Database
{
    /// <summary>
    /// database context
    /// </summary>
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// todo items list
        /// </summary>
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}