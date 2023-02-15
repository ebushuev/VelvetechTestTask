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
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetItems()
        {
            return await _context.TodoItems
                .Select(x => x.ToDto())
                .ToListAsync();
        }

        public async Task<TodoItemDTO> GetItem(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            return item.ToDto();
        }

        public async Task UpdateItem(long id, TodoItemDTO todoItem)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                item.Name = todoItem.Name;
                item.IsComplete = todoItem.IsComplete;
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_context.TodoItems.Any(e => e.Id == id))
            {
                throw new NullReferenceException();
            }
        }

        public async Task<TodoItemDTO> CreateItem(TodoItemDTO item)
        {
            _context.TodoItems.Add(item.ToEntity());
            await _context.SaveChangesAsync();

            return item; //UNDONE так делать нельзя. Надо вернуть сущность из БД
        }

        public async Task DeleteItem(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);

            if (item == null) throw new NullReferenceException();

            _context.TodoItems.Remove(item);
        }
    }
}
