using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.BLL.DTOs.TodoItems;
using TodoApiDTO.BLL.Services.Abstractions;
using TodoApiDTO.DAL.Contexts;
using TodoApiDTO.Models.TodoItems;

namespace TodoApi.Controllers
{
    [Route("api/v1/TodoItems")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoDbContext _context;
        private readonly ITodoItemService _todoItemService;
        private readonly IMapper _mapper;

        public TodoItemsController(
            TodoDbContext context,
            ITodoItemService todoItemService,
            IMapper mapper)
        {
            _context = context;
            _todoItemService = todoItemService;
            _mapper = mapper;
        }

        [HttpGet("GetTodoItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TodoItemResponseModel>>> GetTodoItems()
        {
            var todoItemsResponseDTOs = await _todoItemService.GetAllAsync();

            var todoItemsResponseModels = _mapper.Map<IEnumerable<TodoItemResponseModel>>(todoItemsResponseDTOs);

            return Ok(todoItemsResponseModels);
        }

        [HttpGet("GetTodoItem/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoItemResponseModel>> GetTodoItem(long id)
        {
            var todoItemResponseDTO = await _todoItemService.GetTodoItemByIdAsync(id);

            var todoItemResponseModel = _mapper.Map<TodoItemResponseModel>(todoItemResponseDTO);

            return Ok(todoItemResponseModel);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoItemResponseModel>> CreateTodoItem([FromBody] CreateTodoItemRequestModel requestModel)
        {
            var requestDTO = _mapper.Map<CreateTodoItemRequestDTO>(requestModel);

            var todoItemResponseDTO = await _todoItemService.AddAsync(requestDTO);

            var todoItemResponseModel = _mapper.Map<TodoItemResponseModel>(todoItemResponseDTO);

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemResponseModel.Id }, todoItemResponseModel);
        }

        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTodoItem(long id, [FromBody] UpdateTodoItemRequestModel requestModel)
        {
            var requestDTO = _mapper.Map<UpdateTodoItemRequestDTO>(requestModel);

            await _todoItemService.UpdateAsync(id, requestDTO);

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoItemService.DeleteAsync(id);

            return NoContent();
        }
    }
}
