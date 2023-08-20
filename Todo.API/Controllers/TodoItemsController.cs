using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.Core.Exceptions;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Core.Models.TodoItem;
using Todo.Infrastructure.Entities;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public TodoItemsController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            try
            {
                var result = await _itemService.GetAll();

                return Ok(_mapper.Map<IEnumerable<TodoItemDTO>>(result));
            }
            catch (Exception ex)
            {
                var errorDetails = new ErrorDetails(500, ex.Message);
                return StatusCode(500, errorDetails);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var item = await _itemService.Read(id);

            return Ok(_mapper.Map<TodoItemDTO>(item));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, [FromBody] TodoItemDTOUpdate todoItemDTO)
        {
            var item = await TodoItemExists(id);
            _mapper.Map(todoItemDTO, item);

            await _itemService.Update(item);

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem([FromBody] TodoItemDTOCreate todoItemDTO)
        {
            var toEntity = _mapper.Map<TodoItem>(todoItemDTO);
            await _itemService.Create(toEntity);
            
            var createdItem = _mapper.Map<TodoItemDTO>(toEntity);

            return CreatedAtRoute("TodoItemById", new { todoItemId = createdItem.Id }, createdItem);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var item = await TodoItemExists(id);
            await _itemService.Delete(item);

            return NoContent();
        }

        private async Task<TodoItem> TodoItemExists(long id)
        {
            var item = await _itemService.Read(id);

            if (item is null)
                throw new Exception();

            return item;
        }
    }
}