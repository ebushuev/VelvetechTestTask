using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApiDTO.BLL.DTO;
using TodoApiDTO.BLL.Interfaces;

namespace TodoApiDTO.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {
            var todoItems = await _todoItemService.GetTodoItems();
            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
        {
            var todoItem = await _todoItemService.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDto todoItemDto)
        {
            var result = await _todoItemService.UpdateTodoItem(id, todoItemDto);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItem(TodoItemDto todoItemDto)
        {
            var createdTodoItem = await _todoItemService.CreateTodoItem(todoItemDto);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = createdTodoItem.Id },
                createdTodoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var result = await _todoItemService.DeleteTodoItem(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
