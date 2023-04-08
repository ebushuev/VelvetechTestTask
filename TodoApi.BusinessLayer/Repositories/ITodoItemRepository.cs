using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.BusinessLayer.Models;

namespace TodoApi.BusinessLayer.Repositories
{
    /// <summary>
    /// Methods to create, retrieve, update and delete <see cref="TodoItem"/> in storage
    /// </summary>
    public interface ITodoItemRepository
    {
        /// <summary>
        /// Retrieves all items from the storage
        /// </summary>
        Task<List<TodoItem>> GetAllAsync();

        /// <summary>
        /// Checks if a todo item with given identifier exists in the storage
        /// </summary>
        /// <param name="id">Item identifier to look for</param>
        /// <returns>
        /// <see langword="true"/> if there is an item with this <paramref name="id"/> in the storage,
        /// <see langword="false"/> otherwise
        /// </returns>
        Task<bool> ItemExistsAsync(long id);

        /// <summary>
        /// Find a todo item by its identifier
        /// </summary>
        /// <param name="id">Todo item identifier</param>
        /// <returns>
        /// Stored Todo item with given identifier, or <see langword="null"/> if the item is not found
        /// </returns>
        ValueTask<TodoItem> FindAsync(long id);

        /// <summary>
        /// Saves the item to the storage
        /// </summary>
        /// <param name="item"><see cref="TodoItem"/> to be saved</param>
        /// <returns>Saved todo item with an identifier assigned by the storage</returns>
        Task<TodoItem> AddAsync(TodoItem item);

        /// <summary>
        /// Removes the item from the storage
        /// </summary>
        /// <param name="item"><see cref="TodoItem"/> to be removed</param>
        Task RemoveAsync(TodoItem item);
        
        /// <summary>
        /// Saves changes made in the repository into the storage
        /// </summary>
        Task SaveChangesAsync();
    }
}
