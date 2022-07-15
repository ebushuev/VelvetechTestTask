using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TodoApi.DAL.Entities;

namespace TodoApi.DAL.Repositories
{
    public interface ITodoRepository
    {
        Task<List<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem> GetTodoItemAsync(Expression<Func<TodoItem, bool>> exp);
        Task UpdateTodoItemAsync(TodoItem todoItem);
        Task CreateTodoItemAsync(TodoItem todoItem);
        Task DeleteTodoItemAsync(TodoItem todoItem);

    }
}
