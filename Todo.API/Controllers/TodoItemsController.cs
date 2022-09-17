using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.API.Services;
using Todo.DAL.Models;
using TodoApi.Models;


namespace TodoApi.Controllers
{
    /// <summary>API ��� ������ � ��������</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        /// <summary></summary>
        public TodoItemsController(ITodoService todoService, IMapper mapper)
        {
            _todoService = todoService;
            _mapper = mapper;
        }

        /// <summary>�������� ��� ������</summary>
        /// <response code="200">������ ��������</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var todoItems = await _todoService.GetAsync();
            return _mapper.Map<List<TodoItem>, List<TodoItemDTO>>(todoItems);
        }

        /// <summary>�������� ������</summary>
        /// <param name="id">������������� ������</param>
        /// <response code="200">������ ��������</response>
        /// <response code="404">������ �� �������</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoService.GetAsync(id);
            if (todoItem == null)
                return NotFound();

            return _mapper.Map<TodoItemDTO>(todoItem);
        }

        /// <summary>�������� ������</summary>
        /// <param name="id">������������� ������</param>
        /// <param name="todoItemDTO">������ ������ � ������� ��� ����������</param>
        /// <response code="204">������ ���������</response>
        /// <response code="400">������������ ������: ������� �������������� �� ���������</response>
        /// <response code="404">������ �� �������</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
                return BadRequest();

            try
            {
                var todoItem = _mapper.Map<TodoItem>(todoItemDTO);
                await _todoService.UpdateAsync(todoItem);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>������� ������</summary>
        /// <param name="todoItemDTO">������ ������</param>
        /// <response code="201">������ �������</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemCreateDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDTO);
            await _todoService.AddAsync(todoItem);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                _mapper.Map<TodoItemDTO>(todoItem)
            );
        }

        /// <summary>������� ������</summary>
        /// <param name="id">������������� ������</param>
        /// <response code="204">������ �������</response>
        /// <response code="404">������ �� �������</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                await _todoService.RemoveAsync(id);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
