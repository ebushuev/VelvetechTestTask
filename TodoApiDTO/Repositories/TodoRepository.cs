using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.ApiConstans;
using TodoApiDTO.Repositories.Interfaces;

namespace TodoApiDTO.Repositories
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

        public async Task<ApiResponseStatus> Update(long id, TodoItem model)
        {
            try
            {
                var todoItem = await Get(id);
                if (todoItem == null)
                {
                    return ApiResponseStatus.ItemDoesntExist;
                }
                todoItem.Name = model.Name;
                todoItem.IsComplete = model.IsComplete;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return ApiResponseStatus.ItemDoesntExist;
            }
            return ApiResponseStatus.Success;
        }
        public async Task<TodoItem> Create(TodoItem model)
        {
            _context.TodoItems.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<ApiResponseStatus> Delete(long id)
        {
            var todoItem = await Get(id);

            if (todoItem == null)
            {
                return ApiResponseStatus.ItemDoesntExist;
            }
            
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return ApiResponseStatus.Success;
        }

        private bool TodoItemExists(long id) => _context.TodoItems.Any(todo => todo.Id == id);
    }
}
