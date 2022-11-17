using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodoRepository> _logger;

        public TodoRepository(TodoContext context, ILogger<TodoRepository> logger)
        {
            _context = context;
            _logger = logger;
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
            _logger.LogInformation("Changes are saved successfully");
        }
    }
}