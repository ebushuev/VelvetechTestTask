using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Data;

namespace TodoApiDTO.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        public async Task<TodoItemDTO> Create(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return ItemToDTO(todoItem);
        }

        public async Task<bool> Delete(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem is null)
            {
                return false;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TodoItemDTO>> Get()
        {
            return await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        public async Task<TodoItemDTO?> Get(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem is null)
            {
                return default;
            }

            return ItemToDTO(todoItem);
        }

        public async Task<bool> Update(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem is null)
            {
                return false;
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return false;
            }

            return true;
        }

        private bool TodoItemExists(long id) =>
            _context.TodoItems.Any(e => e.Id == id);

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new()
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}