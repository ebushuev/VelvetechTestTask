using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Models;

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
            return await _todoService.GetList()
                .Select(x => TodoItemDTO.ItemToDTO(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoService.GetById(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return TodoItemDTO.ItemToDTO(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var result = await _todoService.Update(todoItemDTO);

            return GetTodoItemActionResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await _todoService.Create(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                TodoItemDTO.ItemToDTO(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var result = await _todoService.Delete(id);

            return GetTodoItemActionResult(result);
        }
        
        private IActionResult GetTodoItemActionResult(TodoItemActionResult result)
        {
            return result switch
            {
                TodoItemActionResult.Success => NoContent(),
                TodoItemActionResult.NotFound => NotFound(),
                TodoItemActionResult.Failed => BadRequest(),
                _ => Accepted(),
            };
        }
    }
}
