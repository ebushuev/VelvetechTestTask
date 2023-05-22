using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.BLL.DTO;

namespace TodoApiDTO.BLL.Interfaces
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDto>> GetTodoItems();
        Task<TodoItemDto> GetTodoItem(long id);
        Task<bool> UpdateTodoItem(long id, TodoItemDto todoItemDto);
        Task<TodoItemDto> CreateTodoItem(TodoItemDto todoItemDto);
        Task<bool> DeleteTodoItem(long id);
    }
}