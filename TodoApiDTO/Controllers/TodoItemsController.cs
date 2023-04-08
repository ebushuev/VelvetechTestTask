using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.BusinessLayer.Models;
using TodoApi.BusinessLayer.Repositories;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemsController(ITodoItemRepository repository)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Retrieves list of all existing TodoItems.
        /// </summary>
        /// <returns>Array of all TodoItems existing in the database.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return (await _repository.GetAllAsync())
                .Select(x => new TodoItemDTO(x))
                .ToList();
        }

        /// <summary>
        /// Retrieves TodoItem by its identifier.
        /// </summary>
        /// <param name="id">TodoItem identifier in the storage</param>
        /// <returns>TodoItem with the given identifier</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _repository.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return new TodoItemDTO(todoItem);
        }

        /// <summary>
        /// Updates existing TodoItem.
        /// </summary>
        /// <param name="id">Identifier of the item to update</param>
        /// <param name="todoItemDTO">Updated item state</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _repository.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.SetName(todoItemDTO.Name);
            todoItem.SetComplete(todoItemDTO.IsComplete);

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ItemExistsAsync(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Creates new TodoItem in storage.
        /// </summary>
        /// <param name="todoItemDTO">TodoItem to create</param>
        /// <returns>Created TodoItem</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await _repository.AddAsync(
                new TodoItem(todoItemDTO.Name, todoItemDTO.IsComplete));

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                new TodoItemDTO(todoItem));
        }

        /// <summary>
        /// Deletes a TodoItem
        /// </summary>
        /// <param name="id">Identifier of the item to delete</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _repository.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _repository.RemoveAsync(todoItem);

            return NoContent();
        }    
    }
}
