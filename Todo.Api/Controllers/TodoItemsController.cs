using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todo.Common.Commands;
using Todo.Common.Dto;
using Todo.Common.Queries;

namespace Todo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TodoItemDto>>> GetTodoItems(CancellationToken ct)
        {
            return Ok(await _mediator.Send(new GetAllTodosQuery(), ct));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id, CancellationToken ct)
        {
            return Ok(await _mediator.Send(new GetTodoQuery(id), ct));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDto todoItemDto, CancellationToken ct)
        {
            if (id != todoItemDto.Id) return BadRequest();

            await _mediator.Send(new UpdateTodoCommand(todoItemDto), ct);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItem(TodoItemDto todoItemDto, CancellationToken ct)
        {
            var result = await _mediator.Send(new CreateTodoCommand(todoItemDto), ct);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = result.Id },
                result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteTodoCommand(id), ct);

            return NoContent();
        }
    }
}
