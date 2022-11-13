using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.DAL.Models;

namespace TodoApi.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        public Task<TodoItem> GetTodoItemAsync(long id);
        public Task UpdateTodoItemAsync(T todoItem);
        public Task<TodoItem> CreateTodoItemAsync(T todoItem);
        public Task DeleteTodoItemAsync(long id);
    }
}
