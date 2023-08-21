namespace TodoApi.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using TodoApi.ObjectModel.Contracts.Services;
    using TodoApi.ObjectModel.Models.Exceptions;
    using TodoApi.WebApi.Dto;
    using TodoApi.WebApi.Mapping;

    [Route("api/[controller]")]
    [ApiController]
    public sealed class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly ILogger _logger;

        public TodoItemsController(ITodoItemService todoItemService, ILogger<TodoItemsController> logger)
        {
            _todoItemService = todoItemService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems(CancellationToken cancellationToken)
        {
            var items = await _todoItemService.GetItemsAsync(cancellationToken);
            return Ok(items.Select(TodoItemMappings.ItemToDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id, CancellationToken cancellationToken)
        {
            try
            {
                var todoItem = await _todoItemService.GetAsync(id, cancellationToken);
                return TodoItemMappings.ItemToDto(todoItem);
            }
            catch (NotFoundException e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, UpdateTodoItemDto todoItemDto, CancellationToken cancellationToken)
        {
            if (id != todoItemDto.Id)
            {
                var message = $"Specified id parameter '{id}' is not consistent with one in update model '{todoItemDto.Id}'";
                _logger.LogError(message);
                return BadRequest(message);
            }

            try
            {
                await _todoItemService.UpdateAsync(id, TodoItemMappings.DtoToItem(todoItemDto), cancellationToken);
            }
            catch (NotFoundException e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItem(CreateTodoItemDto todoItemDto, CancellationToken cancellationToken)
        {
            var item = TodoItemMappings.DtoToItem(todoItemDto);

            await _todoItemService.CreateAsync(item, cancellationToken);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = item.Id },
                TodoItemMappings.ItemToDto(item));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id, CancellationToken cancellationToken)
        {
            try
            {
                await _todoItemService.DeleteAsync(id, cancellationToken);
            }
            catch (NotFoundException e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }

            return NoContent();
        }
    }
}
