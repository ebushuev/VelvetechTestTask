using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.BLL;
using TodoApiDTO.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemManager _todoManager;

        public TodoItemsController(ITodoItemManager todoManager)
        {
            _todoManager = todoManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return Ok(await _todoManager.GetAllTodoItems());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoManager.GetTodoItemById(id);
            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            await _todoManager.UpdateTodoItem(id, todoItemDTO);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var createdTodoItem = await _todoManager.Create(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = createdTodoItem.Id },
                createdTodoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoManager.DeleteTodoItem(id);
            return NoContent();
        }
    }
}
