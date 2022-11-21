using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.Repository
{
    public class ToDoItemsRepository
    {
        private DataContext _context;

        public ToDoItemsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            try
            {
                return await _context.ToDoItems
                    .ToListAsync();

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public async Task<TodoItem> GetById(long id)
        {
            try
            {
                var item = await _context.ToDoItems
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
                if (item is null)
                {
                    throw new KeyNotFoundException($"Item with ID = {id} not found");
                }
                return item;
            }
            catch (KeyNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TodoItem> Create(TodoItem model)
        {
            try
            {
                var item = await _context.ToDoItems.AddAsync(model);

                await _context.SaveChangesAsync();

                return item.Entity;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        public async Task Update(TodoItem model)
        {
            try
            {
                if (!await _context.ToDoItems.AsNoTracking().AnyAsync(x => x.Id == model.Id))
                {
                    throw new KeyNotFoundException($"Item with ID = {model.Id} not found");
                }
                _context.ToDoItems.Update(model);

                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        public async Task Delete(long id)
        {
            try
            {
                if (!await _context.ToDoItems.AsNoTracking().AnyAsync(x => x.Id == id))
                {
                    throw new KeyNotFoundException($"Item with ID = {id} not found");
                }
                var entity = new TodoItem { Id = id };
                _context.ToDoItems.Attach(entity);
                _context.ToDoItems.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }
    }
}
