using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TodoApi.DAL.EF;
using TodoApi.DAL.Entities;

namespace TodoApi.DAL.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _todoContext;

        public TodoRepository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            return await _todoContext.TodoItems
                .Select(x => x)
                .ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemAsync(Expression<Func<TodoItem, bool>> exp)
        {
            return await _todoContext.TodoItems.FirstOrDefaultAsync(exp);
        }

        public async Task CreateTodoItemAsync(TodoItem todoItem)
        {
            await _todoContext.TodoItems.AddAsync(todoItem);
            await _todoContext.SaveChangesAsync();
        }

        public async Task DeleteTodoItemAsync(TodoItem todoItem)
        {
            _todoContext.TodoItems.Remove(todoItem);
            await _todoContext.SaveChangesAsync();
        }

        public async Task UpdateTodoItemAsync(TodoItem todoItem)
        {
            _todoContext.Update(todoItem);
            await _todoContext.SaveChangesAsync();
        }
    }
}
