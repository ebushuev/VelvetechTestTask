using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private ITodoService _todoService;

        public TodoItemsController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {   
            var todoItems = await _todoService.GetTodoItems();
            return Ok(todoItems.Select(item => item.AsDto()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoService.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _todoService.GetTodoItem(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            await _todoService.UpdateTodoItem(id, todoItemDTO);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            await _todoService.CreateTodoItem(todoItemDTO);
            return Ok(todoItemDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (!await _todoService.TodoItemExists(id))
            {
                return NotFound();
            }
            await _todoService.DeleteTodoItem(id);
            return NoContent();
        }      
    }
}
