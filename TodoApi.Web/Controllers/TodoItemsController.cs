using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Application.Commands.CreateTodoItem;
using TodoApi.Application.Commands.DeleteTodoItem;
using TodoApi.Application.Commands.UpdateTodoItem;
using TodoApi.Application.Dto;
using TodoApi.Application.Queries.GetTodoItem;
using TodoApi.Application.Queries.GetTodoItemList;

namespace TodoApi.Web.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class TodoItemsController : BaseController
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TodoItemDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTodoItemListQuery(), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TodoItemDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTodoItemQuery(id), cancellationToken);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateTodoItem(TodoItemDto todoItemDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new UpdateTodoItemCommand(todoItemDto), cancellationToken);

            return result ? NoContent() : NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(long))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTodoItem(CreateTodoItemDto todoItemDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new CreateTodoItemCommand(todoItemDto), cancellationToken);
            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteTodoItem(long id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteTodoItemCommand(id), cancellationToken);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
