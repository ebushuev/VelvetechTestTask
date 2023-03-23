using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApiDTO.Api.Validation;
using TodoApiDTO.Core.Models;
using TodoApiDTO.Core.Services;

namespace TodoApiDTO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IModelValidator<TodoItemCreateDTO> _createModelValidator;
        private readonly ILogger<TodoItemsController> _logger;
        private readonly ITodoService _service;
        private readonly IModelValidator<TodoItemDTO> _updateModelValidator;

        public TodoItemsController(
            IModelValidator<TodoItemCreateDTO> createModelValidator,
            IModelValidator<TodoItemDTO> updateModelValidator,
            ITodoService service,
            ILogger<TodoItemsController> logger)
        {
            _createModelValidator =
                createModelValidator ?? throw new ArgumentNullException(nameof(createModelValidator));

            _updateModelValidator =
                updateModelValidator ?? throw new ArgumentNullException(nameof(updateModelValidator));

            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            if (id == 2)
            {
                throw new Exception("Test exception.");
            }

            var dto = await _service.FindAsync(id);

            if (dto == null)
            {
                return NotFound();
            }

            return dto;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemCreateDTO dto)
        {
            var validationResult = await _createModelValidator.CheckAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            var createdDto = await _service.CreateAsync(dto);

            _logger.LogInformation("TODO item {id} created {DT}",
                createdDto.Id,
                DateTime.UtcNow.ToLongTimeString());

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = createdDto.Id },
                createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var validationResult = await _updateModelValidator.CheckAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            try
            {
                await _service.UpdateAsync(dto);
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            _logger.LogInformation("TODO item {id} updated {DT}",
                id,
                DateTime.UtcNow.ToLongTimeString());

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _service.GetIsExistAsync(id).Result;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (!await _service.GetIsExistAsync(id))
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);

            _logger.LogInformation("TODO item {id} deleted {DT}",
                id,
                DateTime.UtcNow.ToLongTimeString());

            return NoContent();
        }
    }
}