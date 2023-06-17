using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.BL.DTOs;
using Todo.BL.UseCases.TodoItems.Commands.CreateTodoItem;
using Todo.BL.UseCases.TodoItems.Commands.DeleteTodoItem;
using Todo.BL.UseCases.TodoItems.Commands.UpdateTodoItem;
using Todo.BL.UseCases.TodoItems.Queries.GetTodoItem;
using Todo.BL.UseCases.TodoItems.Queries.GetTodoItems;

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
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetTodoItemsQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetTodoItemQuery(id), cancellationToken));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodoItem([Required] TodoItemDTO request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateTodoItemCommandRequest(request), cancellationToken);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem([FromBody] CreateTodoItemDTO request, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new CreateTodoItemCommandRequest(request), cancellationToken));
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteTodoItem(long id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteTodoItemCommandRequest(id), cancellationToken);
            return NoContent();
        }
    }
}
