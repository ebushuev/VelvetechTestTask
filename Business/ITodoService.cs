using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// Service, that provides the operations under todo items.
    /// </summary>
    public interface ITodoService
    {
        /// <summary>
        /// Creates the item.
        /// </summary>
        /// <param name="item">Todo item DTO</param>
        /// <returns>Created object represented as <see cref="TodoItemDto"/></returns>
        Task<TodoItemDto> CreateItem(TodoItemDto item);

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns><see cref="Task"/></returns>
        Task DeleteItem(long id);

        /// <summary>
        /// Gets item by specified id.
        /// </summary>
        /// <param name="id">The item id.</param>
        /// <returns>Todo item represented as <see cref="TodoItemDto"/></returns>
        Task<TodoItemDto> GetItem(long id);

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns><see cref="IEnumerable{TodoItemDTO}"/></returns>
        Task<IEnumerable<TodoItemDto>> GetItems();

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <param name="todoItem">Updated data.</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateItem(long id, TodoItemDto todoItem);
    }
}