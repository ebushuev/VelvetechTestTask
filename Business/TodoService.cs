using Business.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Business
{
    /// <inheritdoc/>
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItemDto>> GetItems()
        {
            return await _context.TodoItems
                .Select(x => x.ToDto())
                .ToListAsync();
        }

        public async Task<TodoItemDto> GetItem(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            return item.ToDto();
        }

        public async Task UpdateItem(long id, TodoItemDto todoItem)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                item.Name = todoItem.Name;
                item.IsComplete = todoItem.IsComplete;
                await _context.SaveChangesAsync();
            }
            else throw new NullReferenceException("Object not found");
        }

        public async Task<TodoItemDto> CreateItem(TodoItemDto item)
        {
            _context.TodoItems.Add(item.ToEntity());
            await _context.SaveChangesAsync();

            var result = _context.TodoItems.FirstOrDefault(x => x.Name == item.Name);
            return result.ToDto();
        }

        public async Task DeleteItem(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);

            if (item == null) throw new NullReferenceException();

            _context.TodoItems.Remove(item);
        }
    }
}
