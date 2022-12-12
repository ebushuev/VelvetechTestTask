using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Contracts;
using Todo.Domain.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoItemsController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Get all todo items
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<List<TodoItemDTO>> GetTodoItemsAsync(CancellationToken token)
        {
            return _todoService.GetTodoItemsAsync(token);
        }

        /// <summary>
        /// Get todo item by item id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<TodoItemDTO> GetTodoItemAsync(long id, CancellationToken token)
        {
            return _todoService.GetTodoItemAsync(id, token);
        }

        /// <summary>
        /// Update todo item
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO, CancellationToken token)
        {
            await _todoService.UpdateTodoItemAsync(id, todoItemDTO, token);
            return NoContent();
        }

        /// <summary>
        /// Create new todo item
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItemAsync([FromBody] TodoItemDTO todoItemDTO, CancellationToken token)
        {
            var item = await _todoService.CreateTodoItemAsync(todoItemDTO, token);
            return CreatedAtAction(
                nameof(GetTodoItemAsync),
                new { id = item.Id },
                item);
        }

        /// <summary>
        /// Delete todo item by id
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTodoItemAsync(long id, CancellationToken token)
        {
            await _todoService.DeleteTodoItemAsync(id, token);
            return NoContent();
        }
    }
}
