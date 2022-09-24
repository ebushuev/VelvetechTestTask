using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoListService _todoListService;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoListService todoListService, ILogger<TodoItemsController> logger)
        {
            _todoListService = todoListService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return await _todoListService.GetTodoItemsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoListService.GetTodoItemAsync(id);
            if (todoItem == null)
            {
                _logger.LogWarning($"Todo Item with id {id} not found");
                return NotFound();
            }
            return ItemToDTO(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id) return BadRequest();
            try
            {
                if (!await _todoListService.UpdateTodoItemAsync(id, todoItemDTO))
                {
                    _logger.LogWarning($"Unable to update Todo Item with id {id} - item not found");
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException ex) when (!_todoListService.TodoItemExists(id))
            {
                _logger.LogTrace(ex, ex.Message);
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            if (!await _todoListService.CreateTodoItemAsync(todoItemDTO))
            {
                _logger.LogWarning($"Error during saving Todo Item: {todoItem.Id} {todoItem.Name} {todoItem.IsComplete}");
            }

            return CreatedAtAction(
                nameof(GetTodoItem),
                new {id = todoItem.Id},
                ItemToDTO(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (!await _todoListService.DeleteTodoItemAsync(id))
            {
                _logger.LogWarning($"Todo Item with id {id} not found");
                return NotFound();
            }

            return NoContent();
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}