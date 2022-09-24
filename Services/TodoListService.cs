using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TodoApi.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly TodoContext _context;

        public TodoListService(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItemDTO>> GetTodoItemsAsync()
        {
            return await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemAsync(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<bool> UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await GetTodoItemAsync(id);
            if (todoItem == null)
                return false;
            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTodoItemAsync(long id)
        {
            var todoItem = await GetTodoItemAsync(id);
            if (todoItem == null)
                return false;

            _context.TodoItems.Remove(todoItem);
            return await _context.SaveChangesAsync() > 0;
        }
        
        public bool TodoItemExists(long id) =>
            _context.TodoItems.Any(e => e.Id == id);

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}