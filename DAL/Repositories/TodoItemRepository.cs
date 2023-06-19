using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.DAL.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _context;

        public TodoItemRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetById(long todoItemId)
        {
            return await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == todoItemId);
        }

        public async Task Create(TodoItem todoItem)
        {
            await _context.TodoItems.AddAsync(todoItem);
        }

        public void Delete(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
