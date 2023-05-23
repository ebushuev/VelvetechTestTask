using Microsoft.AspNetCore.Mvc;
using Velvetech.MyTodoApp.Application.DTOs;
using Velvetech.MyTodoApp.Application.Services.Abstractions;

namespace TodoApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public ITodoItemService TodoItemService => _todoItemService;

        public TodoItemsController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemReadDto>>> GetTodoItems()
        {
            IEnumerable<TodoItemReadDto> todoItems = await TodoItemService.GetTodoItemsAsync();
            return Ok(todoItems);
        }

        [HttpGet("{id}", Name = "GetTodoItem")]
        public async Task<ActionResult<TodoItemReadDto>> GetTodoItem(Guid id)
        {
            TodoItemReadDto todoItem = await TodoItemService.GetTodoItemAsync(o => o.Id == id);

            if (todoItem is null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(Guid id, TodoItemUpdateDto todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }

            TodoItemReadDto updatedTodoItem = await TodoItemService.UpdateTodoItemAsync(todoItemDto);

            if (updatedTodoItem is null)
            {
                return NotFound();
            }

            return Ok(updatedTodoItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemReadDto>> CreateTodoItem(TodoItemCreateDto todoItemDto)
        {
            TodoItemReadDto createdTodoItem = await TodoItemService.AddTodoItemAsync(todoItemDto);

            return CreatedAtRoute("GetTodoItem", new { id = createdTodoItem.Id }, createdTodoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            bool result = await TodoItemService.DeleteTodoItemAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
