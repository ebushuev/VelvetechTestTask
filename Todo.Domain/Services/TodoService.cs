using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Contracts;
using Todo.Data.Entities;
using Todo.Data.Enums;
using Todo.Data.Repositories;
using Todo.Domain.Exceptions;

namespace Todo.Domain.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<TodoItemDTO> CreateTodoItemAsync(TodoItemDTO todoItemDTO, CancellationToken token)
        {
            var item = _mapper.Map<TodoItem>(todoItemDTO);
            item = await _repository.CreateTodoItemAsync(item, token);
            return _mapper.Map<TodoItemDTO>(item);
        }

        public async Task DeleteTodoItemAsync(long id, CancellationToken token)
        {
            var item = await _repository.GetTodoItemAsync(id, token);
            if (item == null)
            {
                throw new ItemNotFoundException(id);
            }
            await _repository.DeleteItemAsync(id, token);
        }

        public async Task<TodoItemDTO> GetTodoItemAsync(long id, CancellationToken token)
        {
            var item = await _repository.GetTodoItemAsync(id, token);
            if (item == null)
            {
                throw new ItemNotFoundException(id);
            }
            return _mapper.Map<TodoItemDTO>(item);
        }

        public async Task<List<TodoItemDTO>> GetTodoItemsAsync(CancellationToken token)
        {
            var items = await _repository.GetTodoItemsAsync(token);
            return _mapper.Map<List<TodoItemDTO>>(items);
        }

        public async Task UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO, CancellationToken token)
        {
            if (id != todoItemDTO.Id)
            {
                throw new ArgumentException($"{nameof(id)} should be equal {nameof(todoItemDTO.Id)}");
            }
            var todoItem = await _repository.GetTodoItemAsync(id, token);
            if (todoItem == null)
            {
                throw new ItemNotFoundException(id);
            }

            var item = _mapper.Map<TodoItem>(todoItemDTO);
            var result = await _repository.UpdateItemAsync(item, token);
            if (result == UpdateResult.DeleteDuringUpdate)
            {
                throw new ItemNotFoundException(id);
            } else if (result == UpdateResult.UnknownError)
            {
                throw new Exception($"Can't update session with id {id}");
            }
        }
    }
}
