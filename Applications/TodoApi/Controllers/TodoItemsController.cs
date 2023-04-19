using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDto.Services.Interfaces;
using TodoApiDto.StrongId;
using ApiData = TodoApiDto.Shared.Api.Data;
using ServiceData = TodoApiDto.Services.Data;

namespace TodoApi.Controllers
{
    [Route("api/[controller]"), ApiController, Produces("application/json")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public TodoItemsController(
            ITodoService todoService,
            IMapper mapper)
        {
            _todoService = todoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all TodoItems
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ApiData.TodoItemViewModel>>> GetTodoItems()
        {
            var serviceResult = await _todoService.GetAllAsync();

            if (serviceResult.IsError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error" });
            }

            var apiData = _mapper.Map<IReadOnlyCollection<ApiData.TodoItemViewModel>>(serviceResult.Result);

            return Ok(apiData);
        }

        /// <summary>
        /// Get TodoItem by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Record id</param>
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiData.TodoItemViewModel>> GetTodoItem(long id)
        {
            var serviceResult = await _todoService.GetByIdAsync(new TodoId(id));

            if (serviceResult.IsError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error" });
            }

            if (serviceResult.IsNotFound)
            {
                return NotFound();
            }

            var apiData = _mapper.Map<ApiData.TodoItemViewModel>(serviceResult.Result);

            return Ok(apiData);
        }

        /// <summary>
        /// Update TodoItem
        /// </summary>
        /// <param name="id">Record id</param>
        /// <param name="request">Update request</param>
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodoItem(
            long id,
            [FromBody] ApiData.Requests.TodoItemUpdateRequest request)
        {
            if (id != request?.Id)
            {
                return BadRequest();
            }

            var serviceUpdateModel = _mapper.Map<ServiceData.TodoItemUpdateModel>(request);
            var serviceResult = await _todoService.UpdateAsync(serviceUpdateModel);

            if (serviceResult.IsError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error" });
            }

            if (serviceResult.IsNotFound)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Create new TodoItem
        /// </summary>
        /// <param name="request">Create request</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ApiData.TodoItemViewModel>> CreateTodoItem(
            [FromBody] ApiData.Requests.TodoItemCreateRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var serviceCreateModel = _mapper.Map<ServiceData.TodoItemCreateModel>(request);
            var serviceResult = await _todoService.CreateAsync(serviceCreateModel);

            if (serviceResult.IsError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error" });
            }

            var apiData = _mapper.Map<ApiData.TodoItemViewModel>(serviceResult.Result);

            return CreatedAtAction(nameof(GetTodoItem), new { id = apiData.Id }, apiData);
        }

        /// <summary>
        /// Delete TodoItem by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Record id</param>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var serviceResult = await _todoService.RemoveAsync(new TodoId(id));

            if (serviceResult.IsError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error" });
            }

            if (serviceResult.IsNotFound)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}