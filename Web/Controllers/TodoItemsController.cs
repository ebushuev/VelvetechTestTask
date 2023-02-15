using DAL;
using Domain;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.DAL.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ITodoService _service;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoService service, ILogger<TodoItemsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems(CancellationToken token)
        {
            return await _service.GetTodoItemsAsync(token);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id, CancellationToken token)
        {
            var todoItem = await _service.GetTodoItemAsync(id, token);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO, CancellationToken token)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            await _service.UpdateTodoItemAsync(id, todoItemDTO, token);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO, CancellationToken token)
        {
            var result = await _service.CreateTodoItemAsync(todoItemDTO, token);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = result.Id },
                result
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id, CancellationToken token)
        {
            try
            {
                await _service.DeleteTodoItemAsync(id, token);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        private bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);
    }
}
