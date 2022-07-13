using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.Service;
using TodoApiDTO.TodoApiDTO.Infrastructure.DataLayer;
using TodoApiDTO.ToDoApiModels.Models;
using TodoApiDTO.ToDoApiModels.ModelsDTO;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ToDoService _service;

        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ToDoService service, ILogger<TodoItemsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var result = await _service.GetTodoItems();

            if (result != null)
            {
                _logger.LogInformation($"Returned null array of Items on path {HttpContext.Request.Path}");
            }
            else
            {
                _logger.LogInformation($"Returned {result.Count} Item(s) on path {HttpContext.Request.Path}");
            }

            return result;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _service.GetTodoItem(id);

            if (todoItem == null)
            {
                _logger.LogInformation($"Path {HttpContext.Request.Path}: No item found for id: {id}.");
                return NotFound();
            }

            _logger.LogInformation($"Item found for id: {id}.");
            return ItemToDTO(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem =  _service.UpdateTodoItem(id, todoItemDTO);

            if (id != todoItemDTO.Id)
            {
                _logger.LogInformation($"{nameof(UpdateTodoItem)} action: Attempt to modify item failed. ItemID ({todoItemDTO.Id}) does not matches path Id parameter: {id}.");
                return BadRequest();
            }

            if (todoItem == null)
            {
                _logger.LogInformation($"{nameof(UpdateTodoItem)} action: Attempt to modify item failed. ItemID ({todoItemDTO.Id}) has no matches any id on the database: {id}.");
                return NotFound();
            }

            try
            {
                _logger.LogInformation($"{nameof(UpdateTodoItem)} action: Item with {id} has been successfully updated.");
            }
            catch (DbUpdateConcurrencyException e) when (!TodoItemExists(id))
            {
                _logger.LogInformation($"{nameof(UpdateTodoItem)} catch block on db entry update: Item with {id}. - \n{e.Message}");
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await _service.CreateTodoItem(todoItemDTO);

            _logger.LogInformation($"{nameof(CreateTodoItem)} action: New item added to DB: Name: {todoItem.Name}, Completed: {todoItem.IsComplete}");

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _service.DeleteTodoItem(id);

            if (todoItem == null)
            {
                _logger.LogInformation($"{nameof(DeleteTodoItem)} action: Attempt to delete item failed. ItemID ({id}) has no matches in the database.");
                return NotFound();
            }

            _logger.LogInformation($"{nameof(DeleteTodoItem)} action: New item deleted from DB: Name: {todoItem.Name}, Completed: {todoItem.IsComplete}");

            return NoContent();
        }

        private bool TodoItemExists(long id) =>
             _service.TodoItems.Any(e => e.Id == id);

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
