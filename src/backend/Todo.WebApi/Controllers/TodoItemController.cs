using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Threading.Tasks;
using Todo.Domain.DataTransferObjects;
using Todo.Domain.Entities;
using Todo.Services;

namespace Todo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService todoItemService;
        private readonly ILogger<TodoItemController> logger;

        public TodoItemController(ILogger<TodoItemController> logger, ITodoItemService todoItemService)
        {
            this.logger = logger;
            this.todoItemService = todoItemService;
        }

        [HttpGet]
        public async Task<ActionResult> GetTodoItems()
        {
            var currentMethodName = MethodBase.GetCurrentMethod().DeclaringType.Name.Split(new char[] { '<', '>' })[1];
            logger.LogInformation($"Action: {currentMethodName}; IP: {HttpContext.Request.Headers["X-Real-IP"]};");

            var items = await todoItemService.GetAsync();
            return Ok(new { items });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(long id)
        {
            var currentMethodName = MethodBase.GetCurrentMethod().DeclaringType.Name.Split(new char[] { '<', '>' })[1];
            logger.LogInformation($"[{currentMethodName}]: from {HttpContext.Request.Headers["X-Real-IP"]}");

            var item = await todoItemService.GetAsync(id);
            if (item != null)
            {
                return Ok(TodoItemHelper.ItemToDto(item));
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, [FromBody] TodoItemDto dto)
        {
            var currentMethodName = MethodBase.GetCurrentMethod().DeclaringType.Name.Split(new char[] { '<', '>' })[1];
            logger.LogInformation($"[{currentMethodName}]: from {HttpContext.Request.Headers["X-Real-IP"]}");

            var item = await todoItemService.GetAsync(id);
            if (item != null)
            {
                item.Name = dto.Name;
                item.IsComplete = dto.IsComplete;
                await todoItemService.UpdateAsync(id, item);
                return NoContent();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoItem([FromBody] TodoItemDto dto)
        {
            var currentMethodName = MethodBase.GetCurrentMethod().DeclaringType.Name.Split(new char[] { '<', '>' })[1];
            logger.LogInformation($"[{currentMethodName}]: from {HttpContext.Request.Headers["X-Real-IP"]}");

            var item = new TodoItem
            {
                IsComplete = dto.IsComplete,
                Name = dto.Name
            };
            await todoItemService.CreateAsync(item);
            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = item.Id },
                TodoItemHelper.ItemToDto(item));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var currentMethodName = MethodBase.GetCurrentMethod().DeclaringType.Name.Split(new char[] { '<', '>' })[1];
            logger.LogInformation($"[{currentMethodName}]: from {HttpContext.Request.Headers["X-Real-IP"]}");

            var item = await todoItemService.GetAsync(id);
            if (item != null)
            {
                await todoItemService.DeleteAsync(id);
                return NoContent();
            }
            return NotFound();
        }
    }
}