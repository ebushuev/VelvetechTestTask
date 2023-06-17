using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.BLL.Dto;
using TodoApi.BLL.Interfaces;
using TodoApi.DAL;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodoItemsController> _logger;
        private readonly ITodoItemsService _todoItemsService;

        public TodoItemsController(
            TodoContext context,
            ILogger<TodoItemsController> logger,
            ITodoItemsService todoItemsService)
        {
            _context = context;
            _logger = logger;
            _todoItemsService = todoItemsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var items = await _todoItemsService.GetTodoItemsAsync();
            return items.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItemDto = await _todoItemsService.GetTodoItemAsync(id);

            if (todoItemDto == null)
            {
                return NotFound();
            }

            return todoItemDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var updateResult = await _todoItemsService.UpdateTodoItemAsync(id, todoItemDTO);

            if (updateResult)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItemDto = await _todoItemsService.CreateTodoItemAsync(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItemDto.Id },
                todoItemDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var deleteResult = await _todoItemsService.DeleteTodoItemAsync(id);

            if (deleteResult)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
