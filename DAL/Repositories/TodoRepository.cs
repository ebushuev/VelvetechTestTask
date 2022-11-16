using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;
using Domain.Models;
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
        
        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public void Add(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
        }

        public void Remove(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
        }

        public async Task<bool> AnyAsync(long id)
        {
            return await _context.TodoItems.AnyAsync(e => e.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}