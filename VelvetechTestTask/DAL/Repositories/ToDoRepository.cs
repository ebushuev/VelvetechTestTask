using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.DataTransferObjects;
using DAL.Infrastructure;

namespace DAL.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly TodoContext _context;

        public ToDoRepository(TodoContext context) 
        {
            _context = context;   
        }

        public async Task<ToDoItemDTO> CreateAsync(ToDoItemDTO entity)
        {
            var data = await _context.ToDoItems.FirstOrDefaultAsync(e => e.Id == entity.Id);

            if (data != null)
                throw new ValidationException("Data already exist", nameof(data));

            var toDo = await _context.ToDoItems.AddAsync(entity);

            await _context.SaveChangesAsync();

            return toDo.Entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var toDo = await _context.ToDoItems.FirstOrDefaultAsync(a => a.Id == id);

            if (toDo is null)
                throw new ValidationException("ToDo does not exist", nameof(toDo));

            _context.ToDoItems.Remove(toDo);

            await _context.SaveChangesAsync();
        }

        public async Task<ToDoItemDTO> GetAsync(int id)
        {
            var toDo = await _context.ToDoItems.FirstOrDefaultAsync(a => a.Id == id);

            if (toDo is null)
                throw new ValidationException("ToDo does not exist", nameof(toDo));

            return toDo;
        }

        public async Task<List<ToDoItemDTO>> GetAllAsync() => await _context.ToDoItems.ToListAsync();

        public async Task<ToDoItemDTO> UpdateAsync(ToDoItemDTO entity)
        {
            var toDo = await _context.ToDoItems.FirstOrDefaultAsync(a => a.Id == entity.Id);

            if (toDo is null)
                throw new ValidationException("ToDo does not exist", nameof(toDo));

            _context.ToDoItems.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
