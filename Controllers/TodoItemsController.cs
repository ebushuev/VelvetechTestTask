using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Business.DTO;
using TodoApiDTO.Business.Services;
using TodoApiDTO.DataAccess;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ToDoService _toDoService;

        public TodoItemsController(ToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return Ok(await _toDoService.GetTodoItems());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            return await _toDoService.GetTodoItem(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }
            var newToDoItem = await _toDoService.UpdateTodoItem(id, todoItemDTO);
            if (newToDoItem == null)
                return NotFound();
            return Ok(newToDoItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            return await _toDoService.CreateTodoItem(todoItemDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _toDoService.DeleteTodoItem(id);
            return NoContent();
        }

    }
}
