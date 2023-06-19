using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApiDTO.Models;

namespace TodoApiDTO.BLL
{
    public interface ITodoItemManager
    {
        Task<ICollection<TodoItemDTO>> GetAllTodoItems();
        Task<TodoItemDTO> GetTodoItemById(long id);
        Task UpdateTodoItem(long id, TodoItemDTO newTodoItem);
        Task<TodoItemDTO> Create(TodoItemDTO todoItemDto);
        Task DeleteTodoItem(long id);
    }
}
