using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoService _service;
        private readonly IMapper _mapper;

        public TodoItemsController(TodoContext context, IMapper mapper)
        {
            _mapper = mapper;
            _service = new TodoService(context);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var serviceResult = await _service.GetTodoItems();

            if (!serviceResult.Success)
            {
                return StatusCode((int)serviceResult.ResultStatus, new { message = "Internal server error" });
            }

            var todoItemsDTO = _mapper.Map<IEnumerable<TodoItemDTO>>(serviceResult.Result);
            return Ok(todoItemsDTO);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var serviceResult = await _service.GetTodoItem(id);

            if (!serviceResult.Success)
            {
                return NotFound();
            }

            var todoItemDTO = _mapper.Map<TodoItemDTO>(serviceResult.Result);
            return Ok(todoItemDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var getItemServiceResult = await _service.GetTodoItem(id);
            if (!getItemServiceResult.Success)
            {
                return NotFound();
            }

            var updateItemServiceResult = await _service.UpdateTodoItem(getItemServiceResult.Result, todoItemDTO);

            if (!updateItemServiceResult.Success)
            {
                return StatusCode((int)updateItemServiceResult.ResultStatus, new { message = "Internal server error" });
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            var serviceResult = await _service.CreateTodoItem(todoItem);
            var newtodoItemDTO = _mapper.Map<TodoItemDTO>(serviceResult.Result);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                newtodoItemDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var getItemServiceResult = await _service.GetTodoItem(id);
            if (!getItemServiceResult.Success)
            {
                return NotFound();
            }

            var deleteItemServiceResult = await _service.DeleteTodoItem(getItemServiceResult.Result);

            if (!deleteItemServiceResult.Success)
            {
                return StatusCode((int)deleteItemServiceResult.ResultStatus, new { message = "Internal server error" });
            }

            return Ok();
        }   
    }
}
