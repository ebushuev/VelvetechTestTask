using MediatR;
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

        // GET: api/<TodoItemsController>
        [HttpGet]
        public Task<IEnumerable<TodoItemViewModel>> Get(CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetTodoItemsQuery(), cancellationToken);
        }

        // GET api/<TodoItemsController>/5
        [HttpGet("{id}")]
        public Task<TodoItemViewModel> Get([FromRoute] GetTodoItemQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query, cancellationToken);
        }

        // POST api/<TodoItemsController>
        [HttpPost]
        public Task<long> Post([FromBody] CreateTodoItemCommand command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }

        // PUT api/<TodoItemsController>/5
        [HttpPut("{id}")]
        public Task Put([FromRoute] long id, [FromBody] UpdateTodoItemCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            return _mediator.Send(command, cancellationToken);
        }

        // DELETE api/<TodoItemsController>/5
        [HttpDelete("{id}")]
        public Task Delete([FromRoute] long id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new DeleteTodoItemCommand { Id = id }, cancellationToken);
        }
    }
}
