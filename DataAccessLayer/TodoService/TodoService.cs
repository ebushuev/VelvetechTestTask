using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TodoService
    {
        private readonly TodoRepository _repository;

        public TodoService(TodoContext context)
        {
            _repository = new TodoRepository(context);
        }

        public async Task<TodoServiceResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var requestResult = await _repository.GetTodoItems();

            return new TodoServiceResult<IEnumerable<TodoItem>>
            {
                Success = true,
                Result = requestResult.Value ?? new List<TodoItem>(),
                ResultStatus = requestResult.Value is null ? HttpStatusCode.NoContent : HttpStatusCode.OK
            };
        }

        public async Task<TodoServiceResult<TodoItem>> GetTodoItem(long id)
        {
            var requestResult = await _repository.GetTodoItem(id);

            return new TodoServiceResult<TodoItem>
            {
                Success = requestResult != null,
                Result = requestResult is null ? null : requestResult.Value,
                ResultStatus = requestResult is null ? HttpStatusCode.NotFound : HttpStatusCode.OK
            };
        }

        public async Task<TodoServiceResult<IActionResult>> UpdateTodoItem(TodoItem todoItem, TodoItemDTO todoItemDTO)
        {
            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;
            return await TrySaveContext();
        }

        public async Task<TodoServiceResult<IActionResult>> CreateTodoItem(TodoItem todoItem)
        {
            _repository.AddItem(todoItem);
            return await TrySaveContext();
        }

        public async Task<TodoServiceResult<IActionResult>> TrySaveContext()
        {
            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new TodoServiceResult<IActionResult>
                {
                    ResultStatus = HttpStatusCode.InternalServerError
                };
            }

            return new TodoServiceResult<IActionResult>
            {
                Success = true,
                ResultStatus = HttpStatusCode.OK
            };
        }

        public async Task<TodoServiceResult<IActionResult>> DeleteTodoItem(TodoItem todoItem)
        {
            _repository.RemoveItem(todoItem);
            return await TrySaveContext();
        }
    }
}
