using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Models;

namespace TodoApiDTO.BL.Interfaces
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync();
        Task<TodoItemDTO> GetTodoItemAsync( long id );
        Task<TodoItemDTO> CreateTodoItemAsync( TodoItemDTO todoItemDto );
        Task UpdateTodoItemAsync( long id, TodoItemDTO todoItemDto );
        Task DeleteTodoItemAsync( long id );
    }
}
