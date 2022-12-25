using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Operations
{
    public interface ITodoListOperation
    {
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem> GetTodoItemAsync(long id);
        Task SaveChangesAsync();

        void AddTodoItem(TodoItem todoItem);
        void RemoveTodoItem(TodoItem todoItem);
        bool TodoItemExists(long id);
      }
}
