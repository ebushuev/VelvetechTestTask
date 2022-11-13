using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Core.DTOs;
using TodoApi.Core.Requests;

namespace TodoApi.Core.Services.Contract
{
    public interface ITodoItemService
    {
        /// <summary>
        /// Get Todo item list of a certain size and page specified in arguments
        /// </summary>
        /// <param name="pagedTodoItemRequest">Arguments</param>
        /// <returns>Todo item list</returns>
        public Task<ICollection<TodoItemDTO>> GetPagedTodoItems(PagedTodoItemRequest pagedTodoItemRequest);

        /// <summary>
        /// Get Todo item by id
        /// </summary>
        /// <param name="todoItemId"> Todo item id</param>
        /// <returns>Todo item</returns>
        public Task<TodoItemDTO> GetTodoItemById(long todoItemId);

        /// <summary>
        /// Add Todo item
        /// </summary>
        /// <param name="todoItemArgs">Todo item args</param>
        /// <returns>Added Todo item</returns>
        public Task<TodoItemDTO> AddTodoItem(TodoItemArgs todoItemArgs);

        /// <summary>
        /// Update Todo item
        /// </summary>
        /// <param name="todoItemId">Todo item id</param>
        /// <param name="todoItemArgs">Todo item args</param>
        /// <returns></returns>
        public Task UpdateTodoItem(long todoItemId, TodoItemArgs todoItemArgs);

        /// <summary>
        /// Delete Todo item
        /// </summary>
        /// <param name="id"> Todo item id</param>
        /// <returns></returns>
        public Task DeleteTodoItem(long id);
    }
}
