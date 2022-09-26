using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.BLL.Dto;
using TodoApi.BLL.Services;
using TodoApi.BLL.Helper;
using TodoApi.DAL;
using TodoApi.DAL.Entity;

namespace TodoApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase {
        private readonly ITodoRepositoryService _todoRepositoryService;

        public TodoItemsController(ITodoRepositoryService todoRepositoryService) {
            _todoRepositoryService = todoRepositoryService;
        }

        [HttpGet]
        [Route("GetTodoItems")]
        public async Task<IActionResult> GetTodoItems() {
            var todoItems = await _todoRepositoryService.GetTodoItemsAsync();
            return todoItems.AsHttpResult();
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetTodoItem(long id) {
            var todoItem = await _todoRepositoryService.GetTodoItemAsync(id);
            return todoItem.AsHttpResult();
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDto) {
            var updateResult = await _todoRepositoryService.UpdateTodoItemAsync(id, todoItemDto);
            return updateResult.AsHttpResult();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateTodoItem(TodoItemDTO todoItemDto) {
            var todoItem = await _todoRepositoryService.CreateTodoItemAsync(todoItemDto);
            return todoItem.AsHttpResult();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id) {
            var resultDeleted = await _todoRepositoryService.DeleteTodoItem(id);
            return resultDeleted.AsHttpResult();
        }
    }
}