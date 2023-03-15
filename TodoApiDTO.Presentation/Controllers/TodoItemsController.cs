using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoApiDTO.Application.Commands.CreateTodoItem;
using TodoApiDTO.Application.Commands.DeleteTodoItem;
using TodoApiDTO.Application.Commands.UpdateTodoItem;
using TodoApiDTO.Application.Queries;
using TodoApiDTO.Application.Queries.GetTodoItem;
using TodoApiDTO.Application.Queries.GetTodoItems;

namespace TodoApiDTO.Presentation.Controllers
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

        /// <summary>
        /// Get all todo items
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<TodoItemViewModel>> Get(CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetTodoItemsQuery(), cancellationToken);
        }

        /// <summary>
        /// Get todo item by id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<TodoItemViewModel> Get([FromRoute] GetTodoItemQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query, cancellationToken);
        }

        /// <summary>
        /// Create new todo item
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<long> Post([FromBody] CreateTodoItemCommand command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Update todo item
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task Put([FromRoute] long id, [FromBody] UpdateTodoItemCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            return _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Delete todo item by id
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task Delete([FromRoute] long id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new DeleteTodoItemCommand { Id = id }, cancellationToken);
        }
    }
}
