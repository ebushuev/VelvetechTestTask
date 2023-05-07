using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Todo.Common.Models.DTO;
using Todo.Common.ServiceInterfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService todoService;

        public TodoItemsController(ITodoService todoService)
        {
            this.todoService = todoService;
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetItems()
        {
            return Ok(await todoService.GetItemsAsync());
        }

        [HttpGet("item/{id:Guid}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            if (id == null)
            {
                return BadRequest("Id cannot be null");
            }

            var item = await todoService.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateItem(ItemDto item)
        {
            if (item.Id == null)
            {
                return BadRequest("Id cannot be null");
            }

            return Ok(await todoService.UpdateItemAsync(item));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateItem(string name, bool isCompleted = false)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name cannot be empty");
            }

            return Ok(await todoService.CreateItemAsync(name, isCompleted));
        }

        [HttpDelete("delete/{id:Guid}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            if (id == null)
            {
                return BadRequest("Id cannot be null");
            }

            return Ok(await Task.FromResult(todoService.DeleteItemAsync(id)));
        }
    }
}
