using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services
{
    class TodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
            => await _context.TodoItems.ToListAsync();

        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
            => await _context.TodoItems.FindAsync(id);

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void AddItem(TodoItem todoItem)
            => _context.TodoItems.Add(todoItem);

        public void RemoveItem(TodoItem todoItem)
            => _context.TodoItems.Remove(todoItem);

        private bool TodoItemExists(long id)
            => _context.TodoItems.Any(e => e.Id == id);
    }
}
