using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Application.DTO;

namespace Todo.Application.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDTO>> GetItemsAsync();
        Task<TodoItemDTO> FindItemAsync(long id);
        Task UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO);
        Task<TodoItemDTO> CreateItemAsync(TodoItemDTO todoItemDTO);
        Task DeleteItemAsync(long id);
    }
}
