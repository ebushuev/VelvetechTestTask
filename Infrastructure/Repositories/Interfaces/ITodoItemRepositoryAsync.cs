using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ITodoItemRepositoryAsync
    {
        Task<ICollection<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem> GetTodoItemByIdAsync(long itemId);
        Task<TodoItem> InsertTodoItemAsync(TodoItem item);
        Task DeleteTodoItemAsync(long itemId);
        Task UpdateTodoItemAsync(TodoItem item);
        Task SaveAsync();
    }
}
