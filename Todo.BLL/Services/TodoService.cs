using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.BLL.Models;
using Todo.BLL.Validation;
using TodoApi.DAL.Entities;
using TodoApi.DAL.Repositories;

namespace Todo.BLL.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        private readonly TodoItemValidator _todoItemValidator;

        public TodoService(ITodoRepository todoRepository, IMapper mapper, TodoItemValidator todoItemValidator)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
            _todoItemValidator = todoItemValidator;

        }

        public async Task<List<TodoItemDTO>> GetTodoItemsAsync()
        {
            var todos = await _todoRepository.GetTodoItemsAsync();
            return _mapper.Map<List<TodoItemDTO>>(todos);
        }

        public async Task<TodoItemDTO> GetTodoItemByIdAsync(long id)
        {
            var todo = await _todoRepository.GetTodoItemAsync(x=> x.Id == id);
            return _mapper.Map<TodoItemDTO>(todo);
        }

        public async Task CreateTodoItemAsync(TodoItemDTO todoItemdto)
        {
            _todoItemValidator.ValidateAndThrow(todoItemdto);

            var todoItem = _mapper.Map<TodoItem>(todoItemdto);
            await _todoRepository.CreateTodoItemAsync(todoItem);
        }

        public async Task DeleteTodoItemAsync(long id)
        {
            var todoItem = await _todoRepository.GetTodoItemAsync(x => x.Id == id);
            if (todoItem == null)
                throw new Exception($"задачи с id {id} не существует");

            await _todoRepository.DeleteTodoItemAsync(todoItem);
        }

        public async Task UpdateTodoItemAsync(TodoItemDTO todoItemdto)
        {
            _todoItemValidator.ValidateAndThrow(todoItemdto);

            var todoItem = await _todoRepository.GetTodoItemAsync(x => x.Id == todoItemdto.Id);
            if (todoItem == null)
                throw new Exception($"задачи с id {todoItemdto.Id} не существует");

            _mapper.Map(todoItemdto, todoItem);

            await _todoRepository.UpdateTodoItemAsync(todoItem);
        }
    }
}
