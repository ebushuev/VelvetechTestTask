using AutoMapper;
using DAL.DTOs;
using DAL.Entity;
using DAL.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApi.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class TodoItemsController : ControllerBase {
		private readonly ITodoRepository _repo;
		private readonly IMapper _mapper;
		public TodoItemsController(ITodoRepository repo, IMapper mapper) {
			_repo = repo;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems() {
			var todoItems = await _repo.GetAllAsync();
			return _mapper.Map<TodoItemDTO[]>(todoItems);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id) {
			var todoItem = await _repo.GetAsync(id);

			if (todoItem == null) {
				return NotFound();
			}

			return _mapper.Map<TodoItemDTO>(todoItem);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO) {
			if (id != todoItemDTO.Id) {
				return BadRequest();
			}

			var todoItem = await _repo.GetAsync(id);
			if (todoItem == null) {
				return NotFound();
			}
			
			await _repo.UpdateAsync(todoItem);

			return Ok();
		}

		[HttpPost]
		public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO) {
			var todoItem = new TodoItem(todoItemDTO.Name, todoItemDTO.IsComplete);	
			await _repo.SaveAsync(todoItem);

			return CreatedAtAction(
				nameof(GetTodoItem),
				new { id = todoItem.Id },
				_mapper.Map<TodoItemDTO>(todoItem));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTodoItem(long id) {
			var todoItem = await _repo.GetAsync(id);
			if (todoItem == null) {
				return NotFound();
			}

			await _repo.DeleteAsync(todoItem);
			return Ok();
		}
	};
}
