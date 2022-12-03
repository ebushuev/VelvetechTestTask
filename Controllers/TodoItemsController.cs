using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApiDTO.Components.TodoList.Dto;
using TodoApiDTO.Components.TodoList.Services;

namespace TodoApiDTO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoCrudService _todoCrudService;

        public TodoItemsController(TodoCrudService todoCrudService)
        {
            _todoCrudService = todoCrudService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {
            var items = await _todoCrudService.GetTodoItems();

            return new ActionResult<IEnumerable<TodoItemDto>>(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
        {
            return await _todoCrudService.GetTodoItem(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDto todoItemDto)
        {
            await _todoCrudService.UpdateTodoItem(id, todoItemDto);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItem(TodoItemDto todoItemDto)
        {
            var resultDto = await _todoCrudService.CreateTodoItem(todoItemDto);

            return CreatedAtAction(
                nameof(_todoCrudService.GetTodoItem),
                new
                {
                    id = resultDto.Id
                },
                resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoCrudService.DeleteTodoItem(id);
            return Ok();
        }
    }
}