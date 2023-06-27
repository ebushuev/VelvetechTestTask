using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoItemsController(ITodoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets ToDoItems.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TodoItemDTO>))]
        public async Task<IActionResult> GetTodoItems()
        {
            return Ok(await _service.Get());
        }

        /// <summary>
        /// Gets a specific ToDoItem.
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItemDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTodoItem(long id)
        {
            var todoItem = await _service.Get(id);
            
            if (todoItem is null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        /// <summary>
        /// Updates a specific ToDoItem.
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <param name="todoItemDTO">Modified item</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var wasUpdated = await _service.Update(id, todoItemDTO);
            
            if (!wasUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new ToDoItem.
        /// </summary>
        /// <param name="todoItemDTO">New item</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoItemDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await _service.Create(todoItemDTO);
            
            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                todoItem);
        }

        /// <summary>
        /// Deletes a spefific ToDoItem.
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var wasDeleted = await _service.Delete(id);
            
            if (!wasDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
