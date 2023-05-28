using Domain;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TodoItemRepositoryAsync : ITodoItemRepositoryAsync
    {
        private readonly TodoDbContext _context;

        public TodoItemRepositoryAsync(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<TodoItem>> GetTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemByIdAsync(long itemId)
        {
            return await _context.TodoItems.FindAsync(itemId);
        }

        public async Task<TodoItem> InsertTodoItemAsync(TodoItem item)
        {
            var todoItemAdded = await _context.TodoItems.AddAsync(item);
            
            return todoItemAdded.Entity;
        }

        public async Task DeleteTodoItemAsync(long itemId)
        {
            var item = await _context.TodoItems.FindAsync(itemId);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
            }
        }

        public async Task UpdateTodoItemAsync(TodoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual async Task DisposeAsync(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

    }

}
