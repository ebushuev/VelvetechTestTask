using System;
using AutoMapper;
using DAL.DTOs;
using DAL.Entity;
using DAL.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TodoApi.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class TodoItemsController : ControllerBase {
		private readonly ITodoRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger<TodoItemsController> _logger;

		public TodoItemsController(ITodoRepository repo, IMapper mapper, ILogger<TodoItemsController> logger) {
			_repo = repo;
			_mapper = mapper;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems() {
			var todoItems = await _repo.GetAllAsync();
			return _mapper.Map<TodoItemDTO[]>(todoItems);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id) {
			try {
				var todoItem = await _repo.GetAsync(id);
				if (todoItem == null) {
					_logger.LogWarning($"TodoItem with id {id} has not been found!");
					return NotFound();
				}
				return _mapper.Map<TodoItemDTO>(todoItem);

			}
			catch (Exception ex) {
				_logger.LogTrace(ex, ex.Message);
				return BadRequest();
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO) {
			try {
				if (id != todoItemDTO.Id) {
					_logger.LogError($"Passed id {id} and id {todoItemDTO.Id} are not equal!");
					return BadRequest();
				}

				var todoItem = await _repo.GetAsync(id);
				if (todoItem == null) {
					_logger.LogWarning($"TodoItem with id {id} has not been found!");
					return NotFound();
				}
				
				todoItem.Name = todoItemDTO.Name;
				todoItem.IsComplete = todoItem.IsComplete;
				await _repo.UpdateAsync(todoItem);
				return Ok();
			}
			catch (Exception ex) {
				_logger.LogError(ex, ex.Message);
				return BadRequest();
			}
		}

		[HttpPost]
		public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemShortDTO todoItemDTO) {
			try {
				var todoItem = new TodoItem(todoItemDTO.Name, todoItemDTO.IsComplete);
				await _repo.SaveAsync(todoItem);
							
				return CreatedAtAction(
					nameof(GetTodoItem),
					new { id = todoItem.Id },
					_mapper.Map<TodoItemDTO>(todoItem));
			}
			catch (Exception ex) {
				_logger.LogTrace(ex, ex.Message);
				return BadRequest();
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTodoItem(long id) {
			try {
				var todoItem = await _repo.GetAsync(id);
				if (todoItem == null) {
					_logger.LogWarning($"TodoItem with id {id} has not been found!");
					return NotFound();
				}
				await _repo.DeleteAsync(todoItem);
				return Ok();
			}
			catch (Exception ex) {
				_logger.LogTrace(ex, ex.Message);
				return BadRequest();
			}
		}
	};
}
