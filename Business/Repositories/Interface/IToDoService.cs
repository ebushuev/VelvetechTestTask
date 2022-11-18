using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Business.DTO;

namespace TodoApiDTO.Business.Repositories.Interface
{
    public interface IToDoService
    {
        /// <summary>
        /// Get List of ToDo Items
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TodoItemDTO>> GetTodoItems();
        /// <summary>
        /// Get ToDo Item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TodoItemDTO> GetTodoItem(long id);
        /// <summary>
        /// Update ToDo Item
        /// </summary>
        /// <returns></returns>
        Task<TodoItemDTO> UpdateTodoItem(long id, TodoItemDTO todoItemDTO);
        /// <summary>
        /// Create new ToDo Item
        /// </summary>
        /// <param name="todoItemDTO"></param>
        /// <returns></returns>
        Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO);
        /// <summary>
        /// Delete ToDo Item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteTodoItem(long id);
    }
}
