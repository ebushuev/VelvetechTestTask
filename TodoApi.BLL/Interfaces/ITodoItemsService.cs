using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.BLL.Dto;

namespace TodoApi.BLL.Interfaces
{
    public interface ITodoItemsService
    {
        Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync();
        Task<TodoItemDTO> GetTodoItemAsync(long id);
        Task<bool> UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO);
        Task<TodoItemDTO> CreateTodoItemAsync(TodoItemDTO todoItemDTO);
        Task<bool> DeleteTodoItemAsync(long id);
    }
}
