using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _service;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var result = _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(Guid id)
        {
            var todoItem = await _service.GetAsync(id);
            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(Guid id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                var message = $"recieved {nameof(todoItemDTO)} " +
                              $"in {nameof(TodoItemsController.UpdateTodoItem)}" +
                              $"with id {id} which doesn't exist";
                _logger.LogWarning(message);
                return BadRequest();
            }

            var updatedItem = await _service.UpdateAsync(todoItemDTO);
            return Ok(updatedItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            await _service.AddAsync(todoItemDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
