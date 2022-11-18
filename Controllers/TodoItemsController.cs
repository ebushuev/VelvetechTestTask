using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Business.DTO;
using TodoApiDTO.Business.Repositories.Interface;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IToDoService _service;
        private readonly ILoggerService _logger;

        public TodoItemsController(IToDoService service, ILoggerService logger)
        {
            _service = service;
            _logger = logger;
        }
        /// <summary>
        /// Get List of ToDo Items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return Ok(await _service.GetTodoItems());
        }
        /// <summary>
        /// Get ToDo Item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            _logger.LogInfo($"GetToDoItem with id:{id}");
            var todoItem = await _service.GetTodoItem(id);

            if (todoItem == null)
            {
                _logger.LogWarn("No Data Found");
                return NotFound();
            }

            return Ok(todoItem);
        }
        /// <summary>
        /// Update ToDo Item
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            _logger.LogInfo($"UpdateTodoItem with id:{id}");
            if (id != todoItemDTO.Id)
            {
                _logger.LogError("BadRequest data mismatch");
                return BadRequest();
            }
            var todoItem = await _service.UpdateTodoItem(id, todoItemDTO);
            if (todoItem == null)
            {
                _logger.LogWarn("No Data Found");
                return NotFound();
            }
            return NoContent();
        }
        /// <summary>
        /// Create new ToDo Item
        /// </summary>
        /// <param name="todoItemDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            _logger.LogInfo("CreateTodoItem");
            return await _service.CreateTodoItem(todoItemDTO);
        }
        /// <summary>
        /// Delete ToDo Item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            _logger.LogInfo($"DeleteTodoItem with id:{id}");
            var todoItem = await _service.DeleteTodoItem(id);

            if (!todoItem)
            {
                _logger.LogWarn("No Data Found");
                return NotFound();
            }
            return NoContent();
        }
    }
}
