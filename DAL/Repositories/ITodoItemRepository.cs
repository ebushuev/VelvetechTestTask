using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.DAL.Entities;

namespace TodoApiDTO.DAL.Repositories
{
    public interface ITodoItemRepository
    {
        Task<ICollection<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem> GetTodoItemByIdAsync(long id);
        Task InsertTodoItemAsync(TodoItem todoItem);
        Task DeleteTodoItemAsync(long todoItemID);
        Task UpdateTodoItemAsync(TodoItemDTO todoItemDTO);
        Task SaveAsync();
    }
}