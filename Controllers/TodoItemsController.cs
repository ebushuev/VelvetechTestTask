using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Services;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TodoApiDTO.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoItemsController : ControllerBase
	{
		private readonly ITodoItemService _todoItemService;

		public TodoItemsController(ITodoItemService todoItemService)
		{
			_todoItemService = todoItemService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
		{
			return Ok(await _todoItemService.GetTodoItemsAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
		{
			var response = await _todoItemService.GetTodoItemAsync(id);

			return response.State == ItemState.NotFound 
				? (ActionResult<TodoItemDTO>)NotFound() 
				: Ok(response.Result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
		{
			if (id != todoItemDTO.Id)
			{
				return BadRequest();
			}

			var response = await _todoItemService.UpdateTodoItemAsync(todoItemDTO);

			return response.State == ItemState.NotFound 
				? (IActionResult)NotFound() 
				: NoContent();
		}

		[HttpPost]
		public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
		{
			var addResult = await _todoItemService.AddTodoItemAsync(todoItemDTO);

			return CreatedAtAction(nameof(GetTodoItem), new { id = addResult.Id }, addResult);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTodoItem(long id)
		{
			var response = await _todoItemService.DeleteTodoItemAsync(id);

			return response.State == ItemState.NotFound 
				? (IActionResult)NotFound() 
				: NoContent();
		}
	}
}
