using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Models;

namespace TodoApi.Models
{
    public interface ITodoService
    {
        Task<TodoItem> GetById(long itemId);
        Task<TodoItem> Create(TodoItemDTO createInput);
        Task<TodoItemActionResult> Update(TodoItemDTO updateInput);
        Task<TodoItemActionResult> Delete(long itemToDeleteId);
        DbSet<TodoItem> GetList();
    }
}
