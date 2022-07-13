using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Application.TodoItemBoundary.Models;
using TodoApi.Infrastructure.Data.Models;

namespace TodoApiDTO.Application.TodoItemBoundary.Presentation
{
    public interface ITodoItemPresentation
    {
        Task<TodoItemDTO> GetTodoItemDTOByIdAsync(long id);
        Task<IEnumerable<TodoItemDTO>> GetAllAsync();
        Task<TodoItem> CreateTodoItemAsync(TodoItemDTO todoItemDTO);
        Task<TodoItem> GetTodoItemByIdAsync(long id);
        Task DeleteByEntityAsync(TodoItem todoItem);
        bool TodoItemExists(long id);
        Task Update(long id, TodoItemDTO todoItemDTO);
    }
}
