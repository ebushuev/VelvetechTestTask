using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TodoRepository: ITodoRepository
    {
        private readonly DataContext _context;

        public TodoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetAsync(Guid id)
        {
            return await _context.TodoItems.FirstOrDefaultAsync(item=>item.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(item => item.Id == id);
            if (item == null)
            {
                //check, change
            }
            else
            {
              _context.TodoItems.Remove(item);
              await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(TodoItem entity)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(item => item.Id == entity.Id);
            //check null
            _context.TodoItems.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> AddAsync(TodoItem entity)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(item => item.Id == entity.Id);
            //check if exists
             await _context.TodoItems.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
