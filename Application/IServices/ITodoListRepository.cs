using DataAccessLayer.DTOs;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ITodoListRepository
    {
        Task<IEnumerable<TodoItemDTO>> GetTodoItem();

        Task<TodoItemDTO> GetTodoItem(long id);

        Task<bool> UpdateTodoItem(TodoItemDTO todoItemDTO);

        Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO);

        Task<bool> DeleteTodoItem(TodoItem todoItem);

        Task<bool> Save();
    }
}