using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApiDTO.Data.Models;
using TodoApiDTO.Data.Repositories;
using TodoApiDTO.Web.Models;

namespace TodoApiDTO.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoItemRepository todoItemRepository, IUnitOfWork unitOfWork, ILogger<TodoItemsController> logger)
        {
            _todoItemRepository = todoItemRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var items = await _todoItemRepository.GetAll();

            return items.Select(ItemToDto).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoItemRepository.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDto(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }

            var todoItem = await _todoItemRepository.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItemDto.Name;
            todoItem.IsComplete = todoItemDto.IsComplete;

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _todoItemRepository.Exists(id))
                    return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDto)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDto.IsComplete,
                Name = todoItemDto.Name
            };

            await _todoItemRepository.Add(todoItem);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new {id = todoItem.Id},
                ItemToDto(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoItemRepository.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _todoItemRepository.Remove(todoItem);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        private static TodoItemDTO ItemToDto(TodoItem todoItem) =>
            //заменяется на AuotoMapper или аналоги
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}