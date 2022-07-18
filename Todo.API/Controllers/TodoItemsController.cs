using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.BLL.Services;
using TodoApi.BLL;
using Microsoft.Extensions.Logging;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService todoService;
        private readonly ILogger<TodoItemsController> logger;

        public TodoItemsController(ITodoService todoService, ILogger<TodoItemsController> logger)
        {
            this.todoService = todoService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            return await todoService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            // TODO: validate input values
            return await todoService.GetAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            // TODO: validate input values
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }
            await todoService.UpdateAsync(id, todoItemDTO); 
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            // TODO: validate input values
            var id = await todoService.CreateAsync(todoItemDTO);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            // TODO: validate input values
            await todoService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("/LogError")]
        public void LogError()
        {
            logger.LogError(@"aaaaLog to C:\todo-logs");
            throw new Exception("Test Error");
        }
    }
}
