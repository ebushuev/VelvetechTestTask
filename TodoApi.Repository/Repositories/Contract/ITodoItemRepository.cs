using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiRepository.Models;

namespace TodoApiRepository.Repositories.Contract
{
    public interface ITodoItemRepository
    {
        /// <summary>
        /// Get paged TodoItem list
        /// </summary>
        /// <param name="pageSize">Size of the page</param>
        /// <param name="pageNumber">Page number</param>
        /// <returns>Todo items</returns>
        public Task<ICollection<TodoItem>> GetPagedToDoItemsAsync(int pageSize, int pageNumber);
        
        /// <summary>
        /// Find Todo item by id. If nothing is found returns null
        /// </summary>
        /// <param name="id">Todo item id</param>
        /// <returns>Todo item</returns>
        public Task<TodoItem> GetTodoItemByIdAsync(long id);

        /// <summary>
        /// Adds Todo item to db
        /// </summary>
        /// <param name="itemToAdd">Item to add</param>
        /// <returns></returns>
        public Task AddTodoItemAsync(TodoItem itemToAdd);

        /// <summary>
        /// Updates Todo item
        /// </summary>
        /// <param name="itemToUpdate">Updated Todo item</param>
        /// <returns></returns>
        public Task UpdateTododItem(TodoItem itemToUpdate);

        /// <summary>
        /// Delete Todo item
        /// </summary>
        /// <param name="itemToDelete">Item to delete</param>
        /// <returns></returns>
        public Task DeleteTodoItem(TodoItem itemToDelete);
    }
}
