using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.BL.DTO;
using TodoApiDTO.BL.Services;

namespace TodoApi.Controllers.API
{
    /// <summary>
    /// ���������� ��������
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodoItemsController> _logger;
        public TodoItemsController(ITodoService todoService, ILogger<TodoItemsController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        /// <summary>
        /// ��������� ������ �����
        /// </summary>
        /// <returns>������ �����</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            _logger.LogInformation("GetTodoItems");
            var items = await _todoService.GetTodoItems();

            if (items == null)
            {
                _logger.LogError("GetTodoItems NotFound");
                return NotFound();
            }
            return Ok(items);
        }

        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <param name="id">id ������</param>
        /// <returns>������</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            _logger.LogInformation("GetTodoItem");
            var item = await _todoService.GetTodoItem(id);

            if (item == null)
            {
                _logger.LogError("GetTodoItem NotFound");
                return NotFound();
            }
            return Ok(item);
        }

        /// <summary>
        /// ���������� ������
        /// </summary>
        /// <param name="id">id ������</param>
        /// <param name="todoItemDTO">������</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            _logger.LogInformation("UpdateTodoItem");
            var item = await _todoService.UpdateTodoItem(id, todoItemDTO);

            if(item == null)
            {
                _logger.LogError("UpdateTodoItem NotFound");
                return NotFound();
            }
            return Ok(item);
        }

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="todoItemDTO">������</param>
        /// <returns>������</returns>
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            _logger.LogInformation("CreateTodoItem");
            var item = await _todoService.CreateTodoItem(todoItemDTO);

            if (item == null)
            {
                _logger.LogError("CreateTodoItem BadRequest");
                return BadRequest();
            }
            return Ok(item);
        }

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="id">id ������</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            _logger.LogInformation("DeleteTodoItem");
            var result = await _todoService.DeleteTodoItem(id);

            if (!result)
            {
                _logger.LogError("DeleteTodoItem BadRequest");
                return BadRequest();
            }
            return Ok();
        }      
    }
}
