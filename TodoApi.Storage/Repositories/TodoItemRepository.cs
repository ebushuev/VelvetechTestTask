using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.BusinessLayer.Models;
using TodoApi.BusinessLayer.Repositories;
using TodoApi.Storage.Contexts;

namespace TodoApi.Storage.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _context;

        public TodoItemRepository(TodoContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        #region ITodoItemRepository implementation

        /// <inheritdoc/>
        public Task<List<TodoItem>> GetAllAsync()
        {
            var task = _context.TodoItems.ToListAsync();
            task.ConfigureAwait(false);
            return task;
        }

        /// <inheritdoc/>
        public Task<bool> ItemExistsAsync(long id)
        {
            var task = _context.TodoItems.AnyAsync(item => item.Id == id);
            task.ConfigureAwait(false);
            return task;
        }

        /// <inheritdoc/>
        public ValueTask<TodoItem> FindAsync(long id)
        {
            var task = _context.TodoItems.FindAsync(id);
            task.ConfigureAwait(false);
            return task;
        }

        /// <inheritdoc/>
        public async Task<TodoItem> AddAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await SaveChangesAsync();
            return item;
        }

        /// <inheritdoc/>
        public async Task RemoveAsync(TodoItem item)
        {
            _context.TodoItems.Remove(item);
            await SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        #endregion
    }
}
