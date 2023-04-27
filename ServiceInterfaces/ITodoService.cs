using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoApiDTO.Models;

namespace TodoApiDTO.ServiceInterfaces;

public interface ITodoService
{
    Task<TodoItemModel?> GetAsync(long id, CancellationToken token);
    Task<List<TodoItemModel>> GetListAsync(CancellationToken token);
    
    Task<TodoItemModel?> CreateAsync(TodoItemCreateModel model, CancellationToken token);
    Task<TodoItemModel?> SetCompleted(long id, CancellationToken token);
    Task<TodoItemModel?> UpdateAsync(TodoItemUpdateModel model, CancellationToken token);
    Task<bool> DeleteAsync(long id, CancellationToken token);
}