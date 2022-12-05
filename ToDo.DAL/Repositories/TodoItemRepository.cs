using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.DAL.Exceptions;
using ToDo.DAL.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.DAL.Repositories
{
    public class TodoItemRepository : ITodoItemRepository 
    {
        private readonly ITodoDbContext _context;
        
        public TodoItemRepository(ITodoDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            return  await _context.TodoItems.ToListAsync();
        }

        public async Task<ToDoItem> GetAsync(int id)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(todo => todo.Id == id);
            if (item is null)
            {
                throw new NotFoundException(nameof(ToDoItem), id);
            }

            return item;
        }

        public async Task<ToDoItem> CreateAsync(ToDoItem entity)
        {
            var data = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == entity.Id);

            if (data != null)
                throw new AlreadyExistsException("ToDoItem", entity.Id);

            var item = await _context.TodoItems.AddAsync(entity);

            await _context.SaveChangesAsync();

            return item.Entity;
        }

        public async Task<ToDoItem> UpdateAsync(ToDoItem entity)
        {
            var item = await _context.TodoItems.AsNoTracking().FirstOrDefaultAsync(todo => todo.Id == entity.Id);
            if (item is null)
            {
                throw new NotFoundException(nameof(ToDoItem), entity.Id);
            }
            
            _context.TodoItems.Update(entity);

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(a => a.Id == id);

            if (item is null)
                throw new NotFoundException(nameof(ToDoItem), id);

            _context.TodoItems.Remove(item);

            await _context.SaveChangesAsync();
        }
    }
}