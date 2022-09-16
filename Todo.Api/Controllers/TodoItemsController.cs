using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Velvetech.Todo.Api.Models;
using Velvetech.Todo.Logic;
using Velvetech.Todo.Logic.Models;

namespace Velvetech.Todo.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TodoItemsController : ControllerBase
  {
    private readonly ITodoItemService _todoItemService;
    private readonly IMapper _mapper;

    public TodoItemsController(ITodoItemService todoItemService, IMapper mapper)
    {
      _todoItemService = todoItemService;
      _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoItemDTO>), 200)]
    public async Task<IActionResult> GetTodoItems()
    {
      var toDoItems = await _todoItemService.GetAllTodoItemsAsync();

      return Ok(_mapper.Map<IEnumerable<TodoItemDTO>>(toDoItems));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoItemDTO), 200)]
    public async Task<IActionResult> GetTodoItem(long id)
    {
      if (id <= 0)
        throw new ArgumentException("Id must be greater than zero.");

      var todoItem = await _todoItemService.GetTodoItemByIdAsync(id);

      return Ok(_mapper.Map<TodoItemDTO>(todoItem));
    }

    [HttpPut()]
    [ProducesResponseType(typeof(TodoItemDTO), 200)]
    public async Task<IActionResult> UpdateTodoItem(TodoItemDTO todoItemDTO)
    {
      if (todoItemDTO == null)
        throw new ArgumentException("Request body can not be null.");

      if (todoItemDTO.Id <= 0)
        throw new ArgumentException("Id must be greater than zero.");

      var todoItemToUpdate = _mapper.Map<TodoItemModel>(todoItemDTO);

      //Maybe some operations with Secret
      //todoItemToUpdate.Secret = ...

      var updatedTodoItem = await _todoItemService.UpdateTodoItemAsync(todoItemToUpdate);

      return Ok(_mapper.Map<TodoItemDTO>(updatedTodoItem));
    }

    [HttpPost]
    [ProducesResponseType(typeof(TodoItemDTO), 200)]
    public async Task<IActionResult> CreateTodoItem(TodoItemDTO todoItemDTO)
    {
      var todoItemToInsert = _mapper.Map<TodoItemModel>(todoItemDTO);

      //Maybe some operations with Secret
      //todoItemToInsert.Secret = ...

      var insertedTodoItem = await _todoItemService.InsertTodoItemAsync(todoItemToInsert);

      return Ok(_mapper.Map<TodoItemDTO>(insertedTodoItem));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
      if (id <= 0)
        throw new ArgumentException("Id must be greater than zero.");

      await _todoItemService.DeleteTodoItemAsync(id);

      return NoContent();
    }
  }
}
