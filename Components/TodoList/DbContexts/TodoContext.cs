using Microsoft.EntityFrameworkCore;
using TodoApiDTO.Components.TodoList.Models;

namespace TodoApiDTO.Components.TodoList.DbContexts
{
    /// <summary>
    /// Контекст Базы Данных TO-DO.
    /// </summary>
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Набор сущностей TO-DO.
        /// </summary>
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}