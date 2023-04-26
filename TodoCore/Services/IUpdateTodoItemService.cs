using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoCore.DTOs;

namespace TodoCore.Services
{
    public interface IUpdateTodoItemService
    {
        Task<TodoItemDTO> UpdateTodoItemAsync(TodoItemDTO itemDTO);
    }
}
