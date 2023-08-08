using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.BLL.DTO;
using TodoApi.DAL.Entities;

namespace TodoApi.BLL.Interfaces
{
    public interface ITodoService
    {
        Task<List<TodoItemDTO>> GetAllAsync();
        Task<TodoItemDTO> GetAsync(int id);
        Task<TodoItem> AddAsync(TodoItemDTO todoItemDTO);
        Task<bool> UpdateAsync(TodoItemDTO todoItemDTO);
        Task<bool> RemoveAsync(int id);
    }
}
