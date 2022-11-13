using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Core.DTOs;
using TodoApi.Core.Requests;
using TodoApi.Core.Services.Contract;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        /// <summary>
        /// Get Todo item list of a certain size and page specified in arguments
        /// </summary>
        /// <param name="pagedTodoItemRequest">Paged query</param>
        /// <returns>Paged list</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<TodoItemDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<TodoItemDTO>>> GetTodoItems([FromQuery] PagedTodoItemRequest pagedTodoItemRequest)
        {
            return Ok(await _todoItemService.GetPagedTodoItems(pagedTodoItemRequest));
        }

        /// <summary>
        /// Get Todo item by id
        /// </summary>
        /// <param name="id">Todo item id</param>
        /// <returns>Todo item</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoItemDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            return Ok(await _todoItemService.GetTodoItemById(id));
        }

        /// <summary>
        /// Update Todo item
        /// </summary>
        /// <param name="id">Todo item id</param>
        /// <param name="todoItemArgs">Todoo update arguments</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemArgs todoItemArgs)
        {
            await _todoItemService.UpdateTodoItem(todoItemId: id,todoItemArgs: todoItemArgs);

            return NoContent();
        }

        /// <summary>
        /// Add Todo item
        /// </summary>
        /// <param name="todoItemArgs">Todi item args</param>
        /// <returns>Added Todo item</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TodoItemDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemArgs todoItemArgs)
        {
            var todoItem = await _todoItemService.AddTodoItem(todoItemArgs);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                todoItem);
        }

        /// <summary>
        /// Delete Todo item
        /// </summary>
        /// <param name="id">Todo item id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoItemService.DeleteTodoItem(id);

            return NoContent();
        }
    }
}
