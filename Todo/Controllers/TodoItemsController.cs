using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.BLL.Interfaces;
using Todo.DAL.Entities;
using Todo.Dtos;

namespace Todo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoItemService _service;
    private readonly IMapper _mapper;

    public TodoItemsController(ITodoItemService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
    {
        var todoItems = await _service.GetTodoItemsAsync(trackChanges: false);

        return Ok(_mapper.Map<IEnumerable<TodoItemDto>>(todoItems));
    }

    [HttpGet("{todoItemId:guid}", Name = "TodoItemById")]
    public async Task<ActionResult<TodoItemDto>> GetTodoItem(Guid todoItemId)
    {
        var todoItem = await _service.GetTodoItemAsync(todoItemId, trackChanges: false);

        return Ok(_mapper.Map<TodoItemDto>(todoItem));
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTodoItem([FromBody] TodoItemForCreationDto itemDto)
    {
        var todoItemEntity = _mapper.Map<TodoItem>(itemDto);
        await _service.CreateToDoItemAsync(todoItemEntity);

        var createdTodoItem = _mapper.Map<TodoItemDto>(todoItemEntity);
        return CreatedAtRoute("TodoItemById", new { todoItemId = createdTodoItem.Id }, createdTodoItem);
    }

    [HttpPut("{todoItemId:guid}")]
    public async Task<IActionResult> UpdateTodoItem(Guid todoItemId, [FromBody] TodoItemForUpdateDto itemDto)
    {
        var todoItem = await _service.GetTodoItemAsync(todoItemId, trackChanges: false);
        _mapper.Map(itemDto, todoItem);

        await _service.UpdateTodoItemAsync(todoItem);

        return NoContent();
    }

    [HttpDelete("{todoItemId:guid}")]
    public async Task<IActionResult> DeleteTodoItem(Guid todoItemId)
    {
        var todoItem = await _service.GetTodoItemAsync(todoItemId, trackChanges: false);
        await _service.DeleteTodoItemAsync(todoItem);

        return NoContent();
    }
}
