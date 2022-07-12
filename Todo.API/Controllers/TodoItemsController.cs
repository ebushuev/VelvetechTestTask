using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Todo.BLL.DTO;
using Todo.BLL.DTO.Input;
using Todo.DAL;
using Todo.DAL.Managers;
using Todo.DAL.Models;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase<TodoItemsController>
    {
        private readonly TodoItemsManager _manager;

        public TodoItemsController(
            TodoItemsManager manager,
            TodoContext context,
            ILogger<TodoItemsController> logger) : base(context, logger)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var result = await _manager.GetTodoItems()
                                        .Select(x => new TodoItemDTO(x))
                                        .AsNoTracking()
                                        .ToListAsync();

            return Ok(new { result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id)
        {
            var todoItem = await _manager.GetTodoItem(id);
            if (todoItem == null) return NotFound();
            return Ok(new TodoItemDTO(todoItem));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(Guid id, [FromBody] TodoItemInputDTO dto)
        {
            var todoItem = await _manager.GetTodoItem(id);
            if (todoItem == null) return NotFound();

            todoItem.Name = dto.Name;
            todoItem.IsComplete = dto.IsComplete;
            await _manager.UpdateTodoItem(todoItem);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoItem([FromBody] TodoItemInputDTO dto)
        {
            var todoItem = new MTodoItem
            {
                IsComplete = dto.IsComplete,
                Name = dto.Name
            };

            await _manager.CreateTodoItem(todoItem);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                new TodoItemDTO(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            var todoItem = await _manager.GetTodoItem(id);
            if (todoItem == null) return NotFound();

            await _manager.DeleteTodoItem(todoItem);
            return NoContent();
        }
    }
}
