using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.BL.DTOs;
using TodoApp.Common.Contract;
using TodoApp.Common.Logging;
using TodoApp.Data.Entities;
using TodoApp.Data.Todo.Repositories;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<ApiResponse<TodoItemDTO>> AddItem(TodoItemDTO request);
        Task<ApiResponse> Delete(long id);
        Task<ApiResponse<List<TodoItemDTO>>> GetAll();
        Task<ApiResponse<TodoItemDTO>> GetItemById(long id);
        Task<ApiResponse> UpdateItem(long id, TodoItemDTO request);
    }

    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<TodoService> _logger;

        public TodoService(IAppLogger<TodoService> logger, IMapper mapper, ITodoRepository todoRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _todoRepository = todoRepository;
        }

        public async Task<ApiResponse<List<TodoItemDTO>>> GetAll()
        {
            var todoItemEntity = await _todoRepository.GetAll(); 

            if (todoItemEntity == null)
                throw new GlobalException(ResponseCode.DataNotFound);

            var itemList = _mapper.Map<List<TodoItemDTO>>(todoItemEntity);

            return new ApiResponse<List<TodoItemDTO>>(itemList);
        }

        public async Task<ApiResponse<TodoItemDTO>> GetItemById(long id)
        {
            var todoItem = await _todoRepository.Get(id);

            if (todoItem == null)
                throw new GlobalException(ResponseCode.DataNotFound);

            return new ApiResponse<TodoItemDTO>(_mapper.Map<TodoItemDTO>(todoItem));
        }

        public async Task<ApiResponse> UpdateItem(long id, TodoItemDTO request)
        {
            if (id != request.Id)
                throw new GlobalException(ResponseCode.BadRequest);

            var todoItem = await _todoRepository.Get(id);

            if (todoItem == null)
                throw new GlobalException(ResponseCode.DataNotFound);

            todoItem.Name = request.Name;
            todoItem.IsComplete = request.IsComplete;

            _todoRepository.Update(todoItem);

            var saveResult = await _todoRepository.SaveAll();

            if (!saveResult)
                throw new GlobalException(ResponseCode.DBUpdateError);

            return new ApiResponse();
        }

        public async Task<ApiResponse<TodoItemDTO>> AddItem(TodoItemDTO request)
        {
            var item = _mapper.Map<TodoItem>(request);

            if (_todoRepository.HasItem(request.Id))
                throw new GlobalException(ResponseCode.DataAlreadyExist);

            await _todoRepository.AddAsync(item);

            var saveResult = await _todoRepository.SaveAll();

            if (!saveResult)
                throw new GlobalException(ResponseCode.DBUpdateError);

            return new ApiResponse<TodoItemDTO>(_mapper.Map<TodoItemDTO>(item));
        }

        public async Task<ApiResponse> Delete(long id)
        {
            var todoItem = await _todoRepository.Get(id);

            if (todoItem == null)
                throw new GlobalException(ResponseCode.DataNotFound);

            _todoRepository.Remove(todoItem);

            var saveResult = await _todoRepository.SaveAll();

            if (!saveResult)
                throw new GlobalException(ResponseCode.DBUpdateError);

            return new ApiResponse();
        }
    }
}
