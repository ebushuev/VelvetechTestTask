using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Core.Business.TodoItem.Commands.Create;
using Todo.Core.Business.TodoItem.Commands.Delete;
using Todo.Core.Business.TodoItem.Commands.Update;
using Todo.Core.Business.TodoItem.Dto;
using Todo.Core.Business.TodoItem.Queries;

namespace Todo.Web.Controllers
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
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetByIdQuery(id), cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItem([FromBody] CreateCommand request, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(request, cancellationToken));

        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodoItem([FromBody] UpdateCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            await _mediator.Send(new DeleteCommand(id));

            return Ok();
        }
    }
}
