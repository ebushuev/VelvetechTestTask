using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.BLL.Services;
using Todo.BLL.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoItemsController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// �������� ������ �����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<TodoItemDTO>>> GetTodoItems()
        {
            var items = await _todoService.GetTodoItemsAsync();
            if (items == null) return NotFound();

            return items;
        }

        /// <summary>
        /// �������� ������ ����� �� id
        /// </summary>
        /// <param name="id">id ������</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            if (id < 0)
                return BadRequest();

            var item = await _todoService.GetTodoItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return item;
        }

        /// <summary>
        /// �������� ������ �� id
        /// </summary>
        /// <param name="id">id ������</param>
        /// <param name="todoItemDTO">������</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }
            await _todoService.UpdateTodoItemAsync(todoItemDTO);

            return NoContent();
        }

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        /// <param name="todoItemDTO">������</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            if (todoItemDTO == null)
                return BadRequest();

            await _todoService.CreateTodoItemAsync(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItemDTO.Id },
                todoItemDTO);
        }

        /// <summary>
        /// ������� ������ 
        /// </summary>
        /// <param name="id">id ������</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (id <= 0)
                return BadRequest();

            await _todoService.DeleteTodoItemAsync(id);
            return NoContent();
        }
    }
}
