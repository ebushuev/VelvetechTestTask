namespace TodoApiDTO.Components.TodoList.DbContexts
{
    using Microsoft.EntityFrameworkCore;
    using TodoApiDTO.Components.TodoList.Models;

    /// <summary>
    /// Контекст Базы Данных TO-DO.
    /// </summary>
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Набор сущностей TO-DO.
        /// </summary>
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}