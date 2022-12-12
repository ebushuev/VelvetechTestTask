using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.Data.Enums;

namespace Todo.Data.Repositories
{
    public interface ITodoRepository
    {
        Task<TodoItem> GetTodoItemAsync(long id, CancellationToken token);
        Task DeleteItemAsync(long id, CancellationToken token);
        Task<List<TodoItem>> GetTodoItemsAsync(CancellationToken token);
        Task<TodoItem> CreateTodoItemAsync(TodoItem item, CancellationToken token);
        Task<UpdateResult> UpdateItemAsync(TodoItem item, CancellationToken token);
    }
}
