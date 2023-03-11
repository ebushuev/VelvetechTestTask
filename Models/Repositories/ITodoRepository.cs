using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.Models
{
    public interface ITodoRepository
    {
        Task<TodoItem> GetById(long itemId);
        Task<TodoItem> Create(TodoItemDTO input);
        Task<TodoItemActionResult> Update(TodoItemDTO input);
        Task<TodoItemActionResult> Delete(long itemToDeleteId);
        DbSet<TodoItem> GetList();
    }
}
