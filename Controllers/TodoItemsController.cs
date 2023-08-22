using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public sealed class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;        
        private readonly ILogger<TodoItemsController>? _logger;

        public TodoItemsController(ITodoService todoService, ILogger<TodoItemsController>? logger)
        {
            _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));

            _logger = logger;
        }

        /// <summary>List of all todo items</summary>
        /// <returns>Returns the list of all todo items</returns>
        /// <response code="200">Returns the list of all todo items</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems(CancellationToken cancellationToken)
        {
            return await _todoService
                .List()
                .ToListAsync(cancellationToken);
        }

        /// <summary>Get the todo item for the id</summary>
        /// <param name="id">The todo's id to get the todo item for</param>
        /// <returns>The todo item for the id</returns>
        /// <response code="200">The todo item for the id</response>
        /// <response code="404">The todo item for this id not found</response>
        [HttpGet("{id}")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoService.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return TodoItemDTO.From(todoItem);
        }

        /// <summary>Update the todo item for the id</summary>
        /// <param name="id">The todo's id to update the todo item for</param>
        /// <param name="todoItemDTO">The todo's information to update for</param>
        /// <param name="cancellationToken">Cancellation token to monitor cancellation</param>
        /// <response code="204">The todo item for the id updated successfully</response>
        /// <response code="404">The todo item for this id not found</response>
        /// <response code="400">The Id field value in request body doesnt match with id parameter</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO, CancellationToken cancellationToken)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _todoService.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Update(todoItemDTO.Name, todoItemDTO.IsComplete);

            try
            {
                await _todoService.SaveChangesAsync(cancellationToken);

                throw new DbUpdateConcurrencyException();
            }
            catch (DbUpdateConcurrencyException) when (!_todoService.TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>Create new todo item</summary>
        /// <param name="todoItemDTO">The todo's information to create for</param>
        /// <param name="cancellationToken">Cancellation token to monitor cancellation</param>
        /// <returns>The created todo item</returns>
        /// <response code="201">New todo item created successfully</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO, CancellationToken cancellationToken)
        {
            var todoItem = todoItemDTO.ToTodoItem();

            await _todoService.CreateAsync(todoItem, cancellationToken);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                TodoItemDTO.From(todoItem));
        }

        /// <summary>Delete todo item for this id</summary>
        /// <param name="id">The todo's id to delete the todo item for</param>
        /// <param name="cancellationToken">Cancellation token to monitor cancellation</param>
        /// <response code="204">The todo item for the id deleted successfully</response>
        /// <response code="404">The todo item for this id not found</response>        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodoItem(long id, CancellationToken cancellationToken)
        {
            var todoItem = await _todoService.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _todoService.RemoveAsync(todoItem, cancellationToken);

            return NoContent();
        }        
    }
}
