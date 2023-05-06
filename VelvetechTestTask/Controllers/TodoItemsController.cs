using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TodoIData.IServices;
using TodoModels.Models;

namespace TodoApiDTO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ILogger<TodoItemsController> logger, ITodoItemService todoItemService)
        {
            _logger = logger;
            _todoItemService = todoItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            _logger.LogInformation(@"Just getting info when calling ""GetTodoItems();"" method");
            var todoItems = await _todoItemService.GetAllAsync();

            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(long id)
        {
            var todoItemDTO = await _todoItemService.GetByIdAsync(id);

            if (todoItemDTO != null)
            {
                return Ok(todoItemDTO);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var getTodoItemDTO = await _todoItemService.GetByIdAsync(id);
            if (getTodoItemDTO != null)
            {
                await _todoItemService.UpdateAsync(todoItemDTO);
                return Ok("Has been successfully updated!");
            }
            return BadRequest("Provided data is invalid");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            await _todoItemService.AddAsync(todoItemDTO);

            return Ok("Has been successfully created!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var getTodoItemDTO = await _todoItemService.GetByIdAsync(id);
            if (getTodoItemDTO != null)
            {
                await _todoItemService.DeleteAsync(id);
                return Ok("Has been successfully deleted!");
            }
            return BadRequest("Provided data is invalid");
        }
    }
}
