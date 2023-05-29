using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TodoApiDto.Application.Implementations.Command.CreateToDoItem;
using TodoApiDto.Application.Implementations.Command.DeleteTodoItem;
using TodoApiDto.Application.Implementations.Command.UpdateToDoItem;
using TodoApiDto.Application.Implementations.Queries.GetItems;
using TodoApiDto.Application.Implementations.Queries.GetToItem;

namespace TodoApi.Controllers
{
    [Route("testapi")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly IMediator _mediator;

        public TodoItemsController(ILogger<TodoItemsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets item list
        /// </summary>
        /// <returns>Items</returns>

        [HttpGet("items")]
        public async Task<ActionResult> GetTodoItems()
        {
            var result = await _mediator.Send(new GetToDoItemsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Gets item
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Item details</returns>
        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTodoItem([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetToDoItemQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Updates item
        /// </summary>
        /// <param name="id">Item id</param>
        /// <param name="request"></param>
        /// <returns></returns>
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem([FromRoute] Guid id,[FromBody] UpdateToDoItemCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Creates item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        
        [HttpPost]
        public async Task<ActionResult> CreateTodoItem([FromBody] CreateTodoItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { id = result});
        }

        /// <summary>
        /// Deletes item
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Empty result</returns>
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteTodoItemCommand(id));
            return Ok();
        }
    }
}
