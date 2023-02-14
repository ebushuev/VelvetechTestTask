using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Core.Services;
using TodoApiDTO.Dtos;


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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return Ok(await _todoService.GetTodoItemsAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoService.GetTodoItemByIdAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _todoService.GetTodoItemByIdAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _todoService.UpdateTodoItemAsync(todoItemDTO);

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await _todoService.CreateTodoItemAsync(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                _todoService.ItemToDTO(todoItem));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoService.GetTodoItemByIdAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _todoService.DeleteTodoItemAsync(todoItem);

            return NoContent();
        }
    }
}