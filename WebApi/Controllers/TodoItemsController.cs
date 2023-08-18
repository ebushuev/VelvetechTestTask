using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.TodoItems.Commands;
using Application.TodoItems.Models;
using Application.TodoItems.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
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
        public async Task<ActionResult<IEnumerable<TodoItemShortModel>>> GetAllTodoItems()
        {
            var query = new GetAllTodoItems();
            
            var todoItems = await _mediator.Send(query);
            return Ok(todoItems.Select(todoItem => new TodoItemShortModel(todoItem)));
        }
        
        [HttpGet("paginated")]
        public async Task<ActionResult<IEnumerable<TodoItemShortModel>>> GetTodoItemsPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetTodoItemsPaginated(page, pageSize);
            
            var todoItems = await _mediator.Send(query);
            return Ok(todoItems.Select(todoItem => new TodoItemShortModel(todoItem)));
        }

        [HttpGet("{todoItemId}")]
        public async Task<ActionResult<TodoItemShortModel>> GetTodoItem(long todoItemId)
        {
            var query = new GetTodoItemById(todoItemId);

            var todoItem = await _mediator.Send(query);
            return Ok(new TodoItemShortModel(todoItem));
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemShortModel>> CreateTodoItem([FromBody] CreateTodoItemRequest request)
        {
            var command = new CreateTodoItem(request);
            var todo = await _mediator.Send(command);
            return Ok(new TodoItemShortModel(todo));
        }

        [HttpPut("{todoItemId}")]
        public async Task<IActionResult> UpdateTodoItem(long todoItemId, [FromBody] UpdateTodoItemRequest request)
        {
            if (todoItemId != request.Id)
            {
                throw new ArgumentException("Todo Item ID does not match with the updating object");
            }
            
            var command = new UpdateTodoItem(request);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{todoItemId}")]
        public async Task<IActionResult> DeleteTodoItem(long todoItemId)
        {
            var command = new DeleteTodoItem(todoItemId);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}