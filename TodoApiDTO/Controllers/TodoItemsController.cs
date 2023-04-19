using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.BLL.DTO;
using TodoApi.BLL.Interfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        ITodoService todoService;

        public TodoItemsController(ITodoService service)
        {
            todoService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return todoService.GetTodoItems().ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = todoService.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

             todoService.UpdateTodoItem(id, todoItemDTO);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {

            var newItemID = todoService.CreateTodoItem(todoItemDTO);
            todoItemDTO.Id = newItemID;
            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = newItemID },
                todoItemDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = todoService.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            todoService.DeleteTodoItem(id);

            return NoContent();
        }

            
    }
}
