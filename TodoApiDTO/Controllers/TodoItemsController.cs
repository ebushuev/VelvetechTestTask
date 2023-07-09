using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApiDTO.ApiConstans;
using TodoApiDTO.DTOs;
using TodoApiDTO.Extentions;
using TodoApiDTO.Services.Interfaces;

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

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _todoService.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var todoItem = await _todoService.Get(id);

            if (todoItem == null)
            {
                return NotFound(ApiResponseStatus.ItemDoesntExist.GetEnumDescription());
            }

            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, CreateUpdateItemTodoDTO createUpdateDTO)
        {
            var response = await _todoService.Update(id, createUpdateDTO);

            if (response == ApiResponseStatus.ItemDoesntExist)
            {
                return NotFound(response.GetEnumDescription());
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateItemTodoDTO createUpdateDTO)
        {
            var todo = await _todoService.Create(createUpdateDTO);

            return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _todoService.Delete(id);

            if (response == ApiResponseStatus.ItemDoesntExist)
            {
                return NotFound(response.GetEnumDescription());
            }

            return NoContent();
        }
    }
}
