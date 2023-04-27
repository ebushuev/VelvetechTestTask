using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoApiDTO.DAL.Entities;
using TodoApiDTO.Models;

namespace TodoApiDTO.ServiceInterfaces.DatabaseWrappers;

public interface IDataBase
{
    Task<TodoItemEntity?> GetAsync(long id, CancellationToken token);
    Task<List<TodoItemEntity>> GetListAsync(CancellationToken token);
    
    Task<TodoItemEntity?> CreateAsync(TodoItemCreateModel model, CancellationToken token);
    Task<bool> SetCompleted(long id, CancellationToken token);
    Task<bool> UpdateAsync(TodoItemUpdateModel model, CancellationToken token);
    Task<bool> DeleteAsync(long id, CancellationToken token);
}