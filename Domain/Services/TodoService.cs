using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.DAL.Models;

namespace Domain.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository) => _repository = repository;

        public async Task<TodoItemDTO> CreateTodoItemAsync(TodoItemDTO todoItemDTO, CancellationToken token)
        {
            var item = TodoItem(todoItemDTO);

            return TodoItemDTO(await _repository.CreateAsync(item, token));
        }

        public async Task DeleteTodoItemAsync(long id, CancellationToken token)
        {
           await _repository.DeleteAsync(id, token);
        }

        public async Task<TodoItemDTO> GetTodoItemAsync(long id, CancellationToken token)
        {
            return TodoItemDTO(await _repository.GetAsync(id, token));
        }

        public async Task<List<TodoItemDTO>> GetTodoItemsAsync(CancellationToken token)
        {
            var items = await _repository.GetAsync(token);

            return items.Select(x => TodoItemDTO(x)).ToList();
        }

        public async Task UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO, CancellationToken token)
        {
            if (id != todoItemDTO.Id)
            {
                throw new ArgumentException("Bad Argument");
            }

            await _repository.UpdateAsync(TodoItem(todoItemDTO), token);
        }

        // TODO: use automapper
        private static TodoItem TodoItem(TodoItemDTO todoItem) =>
            todoItem != null ? new TodoItem
                {
                    Id = todoItem.Id,
                    Name = todoItem.Name,
                    IsComplete = todoItem.IsComplete
                } : null;

        private static TodoItemDTO TodoItemDTO(TodoItem todoItem) =>
            todoItem != null ? new TodoItemDTO
                {
                    Id = todoItem.Id,
                    Name = todoItem.Name,
                    IsComplete = todoItem.IsComplete
                } : null;
    }
}
