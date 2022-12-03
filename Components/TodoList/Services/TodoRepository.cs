using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApiDTO.Components.TodoList.DbContexts;
using TodoApiDTO.Components.TodoList.Interfaces;
using TodoApiDTO.Components.TodoList.Models;

namespace TodoApiDTO.Components.TodoList.Services
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

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

        public async Task Create(TodoItem item)
        {
            await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TodoItem item)
        {
            var itemToUpdate = await Get(item.Id);

            itemToUpdate.Name = item.Name;
            itemToUpdate.IsComplete = item.IsComplete;
            itemToUpdate.Secret = item.Secret;

            _context.TodoItems.Update(itemToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var itemToDelete = await Get(id);

            if (itemToDelete == null)
            {
                return;
            }

            _context.TodoItems.Remove(itemToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(long id)
        {
            var allItems = await GetAll();

            return allItems
                .Select(x => x.Id)
                .Contains(id);
        }
    }
}