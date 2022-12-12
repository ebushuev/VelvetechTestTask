using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.Data.Enums;

namespace Todo.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodoRepository> _logger;

        public TodoRepository(TodoContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<TodoRepository>();
        }

        public async Task<TodoItem> CreateTodoItemAsync(TodoItem item, CancellationToken token)
        {
            try
            {
                await _context.TodoItems.AddAsync(item, token);
                await _context.SaveChangesAsync(token);
                return item;
            }
            catch (Exception ex)
            {
                LogError(ex, "create");
                throw new Exception($"Error when create session");
            }
        }

        public async Task DeleteItemAsync(long id, CancellationToken token)
        {
            try
            {
                var item = await _context.TodoItems.FirstAsync(i => i.Id == id, token);
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync(token);
            } 
            catch (Exception ex)
            {
                LogError(ex, "delete", id);
                throw new Exception($"Can't delete session with id {id}");
            }
        }

        public Task<TodoItem> GetTodoItemAsync(long id, CancellationToken token)
        {
            return _context.TodoItems.FirstOrDefaultAsync(i => i.Id == id, token);
        }

        public Task<List<TodoItem>> GetTodoItemsAsync(CancellationToken token)
        {
            return _context.TodoItems.ToListAsync(token);
        }

        public async Task<UpdateResult> UpdateItemAsync(TodoItem item, CancellationToken token)
        {
            var todoItem = await _context.TodoItems.FindAsync(item.Id);

            todoItem.Name = item.Name;
            todoItem.IsComplete = item.IsComplete;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                LogError(ex, "update", item.Id);
                var existed = await GetTodoItemAsync(item.Id, token);
                if (existed == null)
                {
                    return UpdateResult.DeleteDuringUpdate;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "update", item.Id);
                return UpdateResult.UnknownError;
            }
            return UpdateResult.Success;
        }

        private void LogError(Exception ex, string operation, long? itemId = null)
        {
            _logger.LogError(ex, $"Error when {operation} todo item %s", itemId);
        }
    }
}
