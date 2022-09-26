using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApi.BLL.Dto;
using TodoApi.BLL.Services;
using TodoApi.DAL;
using TodoApi.DAL.Entity;

namespace TodoApi.BLL.Implementation {
    public class TodoRepository : ITodoRepositoryService {
        private readonly TodoContext _todoContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoRepository> _logger;

        public TodoRepository(TodoContext todoContext, IMapper mapper, ILogger<TodoRepository> logger) {
            _todoContext = todoContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<List<TodoItemDTO>>> GetTodoItemsAsync() {
            try {
                var todoItems = await _todoContext.TodoItems
                    .Select(x => _mapper.Map<TodoItemDTO>(x))
                    .ToListAsync();
                
                return Result.Success(todoItems);
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                return Result.Failure<List<TodoItemDTO>>(e.Message);
            }
        }

        public async Task<Result<TodoItemDTO>> GetTodoItemAsync(long id) {
            var todoItem = await _todoContext.TodoItems.FindAsync(id);

            if (todoItem == null) {
                return Result.Failure<TodoItemDTO>("Отсутствует запись по данному Id");
            }

            return Result.Success(_mapper.Map<TodoItemDTO>(todoItem));
        }

        public async Task<Result> UpdateTodoItemAsync(long id, TodoItemDTO todoItemDto) {
            if (id != todoItemDto.Id) {
                return Result.Failure("Id не совпадают");
            }

            var todoItem = await _todoContext.TodoItems.FindAsync(id);
            if (todoItem == null) {
                return Result.Failure("Отсутствует запись по данному Id");
            }

            todoItem.Name = todoItemDto.Name;
            todoItem.IsComplete = todoItemDto.IsComplete;

            try {
                await _todoContext.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }
        }

        public async Task<Result<TodoItemDTO>> CreateTodoItemAsync(TodoItemDTO todoItemDto) {
            try {
                if (TodoItemExists(todoItemDto.Id)) {
                    return Result.Failure<TodoItemDTO>("Запись с таким Id уже существует");
                }
                _todoContext.TodoItems.Add(_mapper.Map<TodoItem>(todoItemDto));
                await _todoContext.SaveChangesAsync();

                return Result.Success(_mapper.Map<TodoItemDTO>(todoItemDto));
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                return Result.Failure<TodoItemDTO>(e.Message);
            }
        }

        public async Task<Result> DeleteTodoItem(long id) {
            try {
                var todoItem = await _todoContext.TodoItems.FindAsync(id);

                if (todoItem == null) {
                    return Result.Failure("Отсутствует запись по данному Id");
                    ;
                }

                _todoContext.TodoItems.Remove(todoItem);
                await _todoContext.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
                ;
            }
        }
        private bool TodoItemExists(long id) {
            return _todoContext.TodoItems.Any(e => e.Id == id);
        }
    }
}