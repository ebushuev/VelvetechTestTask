using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApiDTO.Models;
using TodoApiDTO.ServiceInterfaces;

namespace TodoApiDTO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoItemsController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemModel>>> GetTodoItems()
    {
        var token = new CancellationTokenSource();

        return await _todoService.GetListAsync(token.Token);
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<TodoItemModel?>> GetTodoItem(long id)
    {
        var token = new CancellationTokenSource();

        return await _todoService.GetAsync(id, token.Token);
    }

    [HttpPut]
    public async Task<TodoItemModel?> UpdateTodoItem(TodoItemUpdateModel model)
    {
        var token = new CancellationTokenSource();

        return await _todoService.UpdateAsync(model, token.Token);
    }

    [HttpPost("create")]
    public async Task<ActionResult<TodoItemModel?>> CreateTodoItem(TodoItemCreateModel model)
    {
        var token = new CancellationTokenSource();

        return await _todoService.CreateAsync(model, token.Token);
    }
    
    [HttpGet("completed/{id}")]
    public async Task<ActionResult<TodoItemModel?>> SetCompleted(long id)
    {
        var token = new CancellationTokenSource();

        return await _todoService.SetCompleted(id, token.Token);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteTodoItem(long id)
    {
        var token = new CancellationTokenSource();

        return await _todoService.DeleteAsync(id, token.Token);
    }
}

