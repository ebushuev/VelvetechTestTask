using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Contracts;

namespace Todo.Domain.Services
{
    public interface ITodoService
    {
        Task<List<TodoItemDTO>> GetTodoItemsAsync(CancellationToken token);
        Task<TodoItemDTO> GetTodoItemAsync(long id, CancellationToken token);
        Task DeleteTodoItemAsync(long id, CancellationToken token);
        Task<TodoItemDTO> CreateTodoItemAsync(TodoItemDTO todoItemDTO, CancellationToken token);
        Task UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO, CancellationToken token);
    }
}
