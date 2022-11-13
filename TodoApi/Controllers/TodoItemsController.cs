using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.BLL.Interfaces;
using TodoApi.BLL.Services;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoItemService todoItemService, ILogger<TodoItemsController> logger)
        {
            _todoItemService = todoItemService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var res = await _todoItemService.GetTodoItemsAsync();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            return await _todoItemService.GetTodoItemAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            await _todoItemService.UpdateTodoItemAsync(id, todoItemDTO);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            return await _todoItemService.CreateTodoItemAsync(todoItemDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoItemService.DeleteTodoItemAsync(id);
            return NoContent();
        } 
    }
}
