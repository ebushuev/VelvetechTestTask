using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.DataAccessLayer;
using TodoApiDTO.BusinessLogicLayer.Interfaces;
using TodoApiDTO.LoggerService;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IToDoService _toDoService;
        private ILoggerManager _logger;

        public TodoItemsController(IToDoService toDoService, ILoggerManager logger)
        {
            _toDoService = toDoService;
            _logger = logger;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return Ok(await _toDoService.GetTodoItemsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var toDoItem = await _toDoService.GetToDoItemByIdAsync(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return Ok(toDoItem);
        }

        [HttpPost]
        public async Task<long> UpsertToDoItem([FromForm] TodoItemDTO todoItemDTO)
        {
            return await _toDoService.UpsertToDoItemsAsync(todoItemDTO);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _toDoService.GetToDoItemByIdAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _toDoService.DeleteToDoItemByIdAsync(todoItem);

            return NoContent();
        }
    }
}
