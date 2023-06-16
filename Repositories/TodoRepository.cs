using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Repositories.Interfaces;

namespace TodoApiDTO.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> Get(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<bool> Update(long id, TodoItem todoItem)
        {
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
        public async Task<TodoItem> Create(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        public async Task Delete(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        private bool TodoItemExists(long id) => _context.TodoItems.Any(e => e.Id == id);
    }
}
