using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Domain.Todo;
using TodoApiDTO.Infrastructure.Database;
using System;

namespace TodoApiDTO.Infrastructure.Services.Todo
{
    public class TodoItemsContainer : ITodoItemsRepository
    {
        private TodoContext _context;

        public TodoItemsContainer(TodoContext todoContext) 
        {
            _context = todoContext;
        }

        public async Task<IEnumerable<TodoItem>> GetItems()
        {
            return await _context.TodoItems.AsNoTracking()
                .ToListAsync();
        }

        public async Task<TodoItem> GetItem(long itemId)
        {
            if(itemId < 1) 
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return await _context.TodoItems.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<long> CreateItem(string name, bool isComplete)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var newItem = new TodoItem(name, isComplete);

            await _context.TodoItems.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return newItem.Id;
        }

        public async Task UpdateItem(TodoItem item)
        {
            if ((item == null) || (item.Id < 1))
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Entry(item).State= EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(TodoItem item)
        {
            if (item.Id < 1)
            {
                throw new ArgumentNullException(nameof(item.Id));
            }

            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
