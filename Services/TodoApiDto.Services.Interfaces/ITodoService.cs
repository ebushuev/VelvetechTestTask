using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDto.Services.Data;
using TodoApiDto.StrongId;

namespace TodoApiDto.Services.Interfaces
{
    /// <summary>
    /// Service for working with TodoItem
    /// </summary>
    public interface ITodoService
    {
        /// <summary>
        /// Get all TodoItems
        /// </summary>
        Task<ServiceResult<IReadOnlyCollection<TodoItem>>> GetAllAsync();

        /// <summary>
        /// Get TodoItem by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Record id</param>
        Task<ServiceResult<TodoItem>> GetByIdAsync(TodoId id);

        /// <summary>
        /// Remove TodoItem by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Record id</param>
        Task<ServiceResult> RemoveAsync(TodoId id);

        /// <summary>
        /// Update TodoItem
        /// </summary>
        /// <param name="updateModel">Update model</param>
        Task<ServiceResult<TodoItem>> UpdateAsync(TodoItemUpdateModel updateModel);

        /// <summary>
        /// Create new TodoItem
        /// </summary>
        /// <param name="createModel">Create model</param>
        Task<ServiceResult<TodoItem>> CreateAsync(TodoItemCreateModel createModel);
    }
}