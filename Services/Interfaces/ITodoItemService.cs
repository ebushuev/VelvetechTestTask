using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task<List<TodoItemDTO>> GetTodoItems();
        Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO);
        Task<TodoItemDTO> GetTodoItem(long id);
        Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO);
        Task<IActionResult> DeleteTodoItem(long id);
    }
}
