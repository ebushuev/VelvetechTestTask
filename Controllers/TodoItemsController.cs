using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.BuisnessLayer.Models;
using TodoApiDTO.BuisnessLayer.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        /// <summary>
        /// �����������
        /// </summary>
        private readonly ILogger<TodoItemsController> _logger;

        /// <summary>
        /// ������ TodoList
        /// </summary>
        private readonly ITodoListService _todoListService;


        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="logger">�����������</param>
        /// <param name="todoListService">������ TodoList</param>
        public TodoItemsController(ILogger<TodoItemsController> logger, ITodoListService todoListService)
        {
            _logger = logger;
            _todoListService = todoListService;
        }

        /// <summary>
        /// ��������� ���� �����
        /// </summary>
        /// <returns>������ ���� �����</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            try
            {
                return await _todoListService.GetTodoItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// ��������� ������ �� ��������������
        /// </summary>
        /// <param name="id">������������� ������</param>
        /// <returns>������</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            try
            {
                var todoItem = await _todoListService.GetTodoItem(id);

                if (todoItem == null)
                {
                    return NotFound();
                }

                return todoItem;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// ���������� ������
        /// </summary>
        /// <param name="id">������������� ������</param>
        /// <param name="todoItemDTO">������</param>
        /// <returns>����������� ������</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            try
            {
                if (id != todoItemDTO.Id)
                {
                    return BadRequest();
                }

                var todoItem = await _todoListService.UpdateTodoItem(id, todoItemDTO);
                if (todoItem == null)
                {
                    return NotFound();
                }

                return Ok(todoItem);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// �������� ����� ������
        /// </summary>
        /// <param name="todoItemDTO">������</param>
        /// <returns>����� ������</returns>
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            try
            {
               var result = await _todoListService.CreateTodoItem(todoItemDTO);
                return CreatedAtAction(
                    nameof(GetTodoItem),
                    new { id = result.Id },
                    result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// �������� ������ �� ��������������
        /// </summary>
        /// <param name="id">������������� ������</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                await _todoListService.DeleteTodoItem(id);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
