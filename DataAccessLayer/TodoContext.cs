using Microsoft.EntityFrameworkCore;
using TodoApiDTO.DataAccessLayer.Models;

namespace TodoApiDTO.DataAccessLayer
{
    /// <summary>
    /// Контекст БД
    /// </summary>
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Таблица списков задач
        /// </summary>
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}