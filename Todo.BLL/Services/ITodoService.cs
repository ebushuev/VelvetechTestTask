using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.BLL.Models;
using TodoApi.DAL.Entities;

namespace Todo.BLL.Services
{
    public interface ITodoService
    {
        Task<List<TodoItemDTO>> GetTodoItemsAsync();
        Task<TodoItemDTO> GetTodoItemByIdAsync(long id);

        Task UpdateTodoItemAsync(TodoItemDTO todoItem);
        Task CreateTodoItemAsync(TodoItemDTO todoItem);
        Task DeleteTodoItemAsync(long id);
    }
}
