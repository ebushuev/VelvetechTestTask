using BusinessLayer.DTOs;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interface
{
    public interface ITodoItemService
    {

        Task<IEnumerable<TodoItemDTO>> GetTodoItems();
        Task<TodoItemDTO> GetTodoItem(long id);
        bool TodoItemExists(long id);
        void UpdateTodoItem(long id,CreateOrUpdateTodoItemDTO item);
        Task<long> CreateTodoItem(CreateOrUpdateTodoItemDTO item);
        void DeleteTodoItem(long id);

    }
}
