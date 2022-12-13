using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.BL.DTOs;
using TodoApp.Common.Base;
using TodoApp.Common.Contract;
using TodoApp.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : BaseController<TodoItemsController>
    {
        private readonly ITodoService _todoService;

        public TodoItemsController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetTodoItems() =>
            await _todoService.GetAll();

        [HttpGet("{id}")]
        public async Task<ApiResponse<TodoItemDTO>> GetTodoItem(long id) =>
            await _todoService.GetItemById(id);

        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateTodoItem(long id, TodoItemDTO todoItemDTO) =>
            await _todoService.UpdateItem(id, todoItemDTO);

        [HttpPost]
        public async Task<ApiResponse<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO) =>
            await _todoService.AddItem(todoItemDTO);

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteTodoItem(long id) =>
            await _todoService.Delete(id); 
    }
}
