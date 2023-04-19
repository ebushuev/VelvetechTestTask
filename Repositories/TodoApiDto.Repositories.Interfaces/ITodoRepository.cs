using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDto.Repositories.Data;
using TodoApiDto.StrongId;

namespace TodoApiDto.Repositories.Interfaces
{
    /// <summary>
    /// Repository for working with TodoItem
    /// </summary>
    public interface ITodoRepository
    {
        /// <summary>
        /// Get all TodoItems
        /// </summary>
        Task<IReadOnlyCollection<TodoItem>> GetAllAsync();

        /// <summary>
        /// Get TodoItem by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Record id</param>
        Task<TodoItem> GetByIdAsync(TodoId id);

        /// <summary>
        /// Remove TodoItem by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Record id</param>
        Task RemoveAsync(TodoId id);

        /// <summary>
        /// Update TodoItem
        /// </summary>
        /// <param name="updateModel">Update model</param>
        Task<TodoItem> UpdateAsync(TodoItemUpdateModel updateModel);

        /// <summary>
        /// Create new TodoItem
        /// </summary>
        /// <param name="createModel">Create model</param>
        Task<TodoItem> CreateAsync(TodoItemCreateModel createModel);
    }
}