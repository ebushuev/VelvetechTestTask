using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.Interfaces
{
    public interface ITodoItemService
    {
        Task<List<TodoItemDTO>> GetTodoItems();
        Task<TodoItemDTO> GetTodoItem(long id);
        Task<bool> UpdateTodoItem(long id, TodoItemDTO todoItemDTO);
        Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO);
        Task<bool> DeleteTodoItem(long id);
    }
}