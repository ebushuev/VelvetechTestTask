using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApiDTO.Models;
using TodoApiDTO.Data;

namespace TodoApiDTO.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return (IEnumerable<TodoItem>)await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemAsync(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public void CreateTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
        }

        public void DeleteTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);
    }
}
