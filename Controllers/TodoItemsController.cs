using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public TodoItemsController(TodoContext context, ITodoService todoService, IMapper mapper)
        {
            _context = context;
            _todoService = todoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var items = await _todoService.GetAllTodoItemsAsync();
            var mappedItems = _mapper.Map<List<TodoItemDTO>>(items);

            return mappedItems;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoService.GetTodoItemByIdAsync(id);

            if (todoItem == null)
                return NotFound();

            var result = _mapper.Map<TodoItemDTO>(todoItem);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
                return BadRequest();

            var isExists = await _todoService.IsTodoItemExistsAsync(id);
            if (!isExists)
                return NotFound();

            var itemToUpdate = _mapper.Map<TodoItem>(todoItemDTO);

            try
            {
                await _todoService.UpdateTodoItemAsync(itemToUpdate);
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
            if (todoItemDTO.Id != 0)
                return BadRequest();

            var todoItem = _mapper.Map<TodoItem>(todoItemDTO);

            await _todoService.AddTodoItemAsync(todoItem);
            var dto = _mapper.Map<TodoItemDTO>(todoItem);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var isExists = await _todoService.IsTodoItemExistsAsync(id);
            if (!isExists)
                return NotFound();

            await _todoService.DeleteTodoItemAsync(id);

            return NoContent();
        }

        private bool TodoItemExists(long id) =>
             _todoService
            .IsTodoItemExistsAsync(id)
            .GetAwaiter()
            .GetResult();   
    }
}
