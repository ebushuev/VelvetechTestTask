using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.DataAccessLayer
{
    public class DataAccessLayerImplementation : IDataAccessLayer
    {
        private readonly TodoContext _context;

        public DataAccessLayerImplementation(TodoContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        public async Task DeleteTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> GetTodoItem(long id)
        {
            return await _context
                .TodoItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<List<TodoItem>> GetTodoItems()
        {
            return await _context.TodoItems.AsNoTracking().ToListAsync();
        }

        public async Task UpdateTodoItem(TodoItemDTO todoItemDTO)
        {
            TodoItem todoItem = _context.TodoItems.First(x => x.Id == todoItemDTO.Id);

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            await _context.SaveChangesAsync();
        }
    }
}
