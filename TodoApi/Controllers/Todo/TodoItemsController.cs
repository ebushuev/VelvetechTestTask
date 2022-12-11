using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Controllers.Todo.Mapping;
using TodoApi.Controllers.Todo.Models;
using TodoApiDTO.Application.Features.TodoItems.GetTodoItem;
using TodoApiDTO.Application.Features.TodoItems.GetTodoItems;
using TodoApiDTO.Application.Features.TodoItems.RemoveTodoItem;
using TodoApiDTO.Application.Features.TodoItems.UpdateTodoItem;

namespace TodoApi.Controllers.Todo;

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
    public async Task<GetTodoItemsResponse> GetTodoItems(
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 25,
        CancellationToken cancellationToken = default)
    {
        var query = new GetTodoItemsQuery(pageNumber, pageSize);

        var result = await _mediator.Send(query, cancellationToken);
        return result.MapToResponse();
    }

    [HttpGet("{id}")]
    public async Task<TodoItemDto> GetTodoItem(
        long id,
        CancellationToken cancellationToken)
    {
        var query = new GetTodoItemQuery(id);

        var result = await _mediator.Send(query, cancellationToken);

        return result.MapToDto();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoItem(
        long id,
        TodoItemDto todoItemDto,
        CancellationToken cancellationToken)
    {
        if (id != todoItemDto.Id) return BadRequest();

        var command = new UpdateTodoItemQuery(todoItemDto.MapToModel());

        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> CreateTodoItem(
        CreateTodoItemRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.MapToQuery();

        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetTodoItem),
            new { id = result.Id },
            result.MapToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(
        long id,
        CancellationToken cancellationToken)
    {
        var command = new RemoveTodoItemQuery(id);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}