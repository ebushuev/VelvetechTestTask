using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.BLL.Models;
using TodoApi.BLL.Services.Contracts;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly IServiceTodoItem _service;

    public TodoItemsController(IServiceTodoItem service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
    {
        var todoItems = await _service.GetTodoItems();
        return Ok(todoItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
    {
        var todoItem = await _service.GetTodoItemById(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return Ok(todoItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDto todoItemDTO)
    {
        todoItemDTO.Id = id;

        var updateResult = await _service.UpdateTodoItem(todoItemDTO);

        if (!updateResult)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> CreateTodoItem(TodoItemDto todoItemDTO)
    {
        return await _service.CreateTodoItem(todoItemDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        var deleteIsSuccess = await _service.DeleteTodoItem(id);

        if (!deleteIsSuccess)
        {
            return NotFound();
        }

        return NoContent();
    }
}