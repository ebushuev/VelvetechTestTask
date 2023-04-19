using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDto.Repositories.Context;
using TodoApiDto.Repositories.Data;
using TodoApiDto.Repositories.Interfaces;
using TodoApiDto.Shared.Helpers;
using TodoApiDto.StrongId;

namespace TodoApiDto.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<TodoItem>> GetAllAsync()
            => await _context.TodoItems.AsNoTracking().ToListAsync();

        public async Task<TodoItem> GetByIdAsync(TodoId id)
        {
            id.ThrowIfNull(nameof(id));

            return await _context.TodoItems
                .Where(todoItem => todoItem.Id == id.ObjectId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(TodoId id)
        {
            id.ThrowIfNull(nameof(id));

            try
            {
                var item = new TodoItem
                {
                    Id = id.ObjectId,
                };

                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (await _context.TodoItems.AnyAsync(i => i.Id == id.ObjectId))
                {
                    throw;
                }
            }
        }

        public async Task<TodoItem> UpdateAsync(TodoItemUpdateModel updateModel)
        {
            updateModel.ThrowIfNull(nameof(updateModel));

            var dbTodoItem = await _context.TodoItems
                .FirstOrDefaultAsync(todoItem => todoItem.Id == updateModel.Id);

            dbTodoItem.Name = updateModel.Name;
            dbTodoItem.IsComplete = updateModel.IsComplete;

            await _context.SaveChangesAsync();

            return dbTodoItem;
        }

        public async Task<TodoItem> CreateAsync(TodoItemCreateModel createModel)
        {
            createModel.ThrowIfNull(nameof(createModel));

            var todoItem = new TodoItem
            {
                IsComplete = createModel.IsComplete,
                Name = createModel.Name,
            };

            var dbTodoItem = _context.TodoItems.Add(todoItem);

            await _context.SaveChangesAsync();

            return dbTodoItem.Entity;
        }
    }
}