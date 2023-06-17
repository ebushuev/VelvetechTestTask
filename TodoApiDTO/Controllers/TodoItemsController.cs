using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.DTOs;
using TodoApiDTO.Services.Interfaces;

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
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAll() => Ok(await _todoService.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> Get(long id)
        {
            var todoItem = await _todoService.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(long id, CreateUpdateItemTodoDTO createUpdateDTO)
        {
            var isFound = await _todoService.Update(id, createUpdateDTO);

            if (isFound == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> Create(CreateUpdateItemTodoDTO createUpdateDTO)
        {
            var todo = await _todoService.Create(createUpdateDTO);

            return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var isFound = await _todoService.Delete(id);

            if (isFound == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
