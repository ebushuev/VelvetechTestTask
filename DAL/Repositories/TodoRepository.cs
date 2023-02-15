using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TodoApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> CreateAsync(TodoItem item, CancellationToken token)
        {
            var result = await _context.TodoItems.AddAsync(item, token);
            await _context.SaveChangesAsync(token);

            return result.Entity;
        }

        public async Task DeleteAsync(long id, CancellationToken token)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                throw new ArgumentException("NotFound");
            }

            _context.TodoItems.Remove(todoItem);

            await _context.SaveChangesAsync();
        }

        public Task<TodoItem> GetAsync(long id, CancellationToken token)
        {
            return _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public Task<List<TodoItem>> GetAsync(CancellationToken token)
        {
            return _context.TodoItems.ToListAsync(token);
        }

        public async Task<TodoItem> UpdateAsync(TodoItem item, CancellationToken token)
        {
            var itemToUpdate = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == item.Id, token);

            if (itemToUpdate == null)
            {
                throw new ArgumentException();
            }

            itemToUpdate.Name = item.Name;
            itemToUpdate.IsComplete = item.IsComplete;
            itemToUpdate.Secret = item.Secret;

            await _context.SaveChangesAsync(token);

            return itemToUpdate;
        }
    }
}
