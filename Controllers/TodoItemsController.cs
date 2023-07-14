using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.IToDoServices;
namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private IToDoService _toDoService;
        private readonly IMapper _mapper;
        protected readonly ILogger _logger;
        public TodoItemsController(IToDoService toDoService, IMapper mapper, ILogger<TodoItemsController> logger)
        {
            _toDoService = toDoService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            _logger.LogInformation("Hello From logging");
            var result = _toDoService._todoRepository.FindAll().ToList();
            return _mapper.Map<List<TodoItemDTO>>(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = _toDoService._todoRepository.FindByCondition(x => x.Id == id).First();

            if (todoItem == null)
            {
                return NotFound();
            }

            return _mapper.Map<TodoItemDTO>(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = _toDoService._todoRepository.FindByCondition(x => x.Id == id).First();
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                _toDoService._todoRepository.Update(todoItem);
                _toDoService.Save();

            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _toDoService._todoRepository.Create(todoItem);
            _toDoService.Save();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = _toDoService._todoRepository.FindByCondition(x => x.Id == id).First();

            if (todoItem == null)
            {
                return NotFound();
            }

            _toDoService._todoRepository.Delete(todoItem);
            _toDoService.Save();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            var result = _toDoService._todoRepository.FindByCondition(x => x.Id == id).First();
            
            if(result != null)
            {
                return true;
            }
            return false;   
        }
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };       
    }
}
