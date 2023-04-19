using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDto.Repositories.Interfaces;
using TodoApiDto.Services.Interfaces;
using TodoApiDto.Shared.Helpers;
using TodoApiDto.StrongId;
using DbData = TodoApiDto.Repositories.Data;
using ServiceData = TodoApiDto.Services.Data;

namespace TodoApiDto.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoService(
            ITodoRepository todoRepository,
            IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<ServiceData.ServiceResult<IReadOnlyCollection<ServiceData.TodoItem>>> GetAllAsync()
        {
            var dbData = await _todoRepository.GetAllAsync();

            var serviceData = _mapper.Map<IReadOnlyCollection<ServiceData.TodoItem>>(dbData);

            return new ServiceData.ServiceResult<IReadOnlyCollection<ServiceData.TodoItem>>
            {
                Result = serviceData ?? new List<ServiceData.TodoItem>(),
                IsSuccess = true,
            };
        }

        public async Task<ServiceData.ServiceResult<ServiceData.TodoItem>> GetByIdAsync(TodoId id)
        {
            id.ThrowIfNull(nameof(id));

            var dbTodoItem = await _todoRepository.GetByIdAsync(id);

            return new ServiceData.ServiceResult<ServiceData.TodoItem>
            {
                Result = _mapper.Map<ServiceData.TodoItem>(dbTodoItem),
                IsSuccess = dbTodoItem is null,
                IsNotFound = dbTodoItem is null,
            };
        }

        public async Task<ServiceData.ServiceResult> RemoveAsync(TodoId id)
        {
            id.ThrowIfNull(nameof(id));

            var dbTodoItem = await _todoRepository.GetByIdAsync(id);

            if (dbTodoItem is null)
            {
                return new ServiceData.ServiceResult
                {
                    IsSuccess = false,
                    IsError = false,
                    IsNotFound = true,
                };
            }

            await _todoRepository.RemoveAsync(id);

            return new ServiceData.ServiceResult
            {
                IsSuccess = true,
            };
        }

        public async Task<ServiceData.ServiceResult<ServiceData.TodoItem>> UpdateAsync(
            ServiceData.TodoItemUpdateModel updateModel)
        {
            updateModel.ThrowIfNull(nameof(updateModel));

            var dbTodoItem = await _todoRepository.GetByIdAsync(updateModel.Id);

            if (dbTodoItem is null)
            {
                return new ServiceData.ServiceResult<ServiceData.TodoItem>
                {
                    IsSuccess = false,
                    IsError = false,
                    IsNotFound = true,
                };
            }

            var dbUpdateModel = _mapper.Map<DbData.TodoItemUpdateModel>(updateModel);

            var updatedDbTodoItem = await _todoRepository.UpdateAsync(dbUpdateModel);

            return new ServiceData.ServiceResult<ServiceData.TodoItem>
            {
                Result = _mapper.Map<ServiceData.TodoItem>(updatedDbTodoItem),
                IsSuccess = true,
            };
        }

        public async Task<ServiceData.ServiceResult<ServiceData.TodoItem>> CreateAsync(
            ServiceData.TodoItemCreateModel createModel)
        {
            createModel.ThrowIfNull(nameof(createModel));

            var dbCreateModel = _mapper.Map<DbData.TodoItemCreateModel>(createModel);

            var createdDbTodoItem = await _todoRepository.CreateAsync(dbCreateModel);

            return new ServiceData.ServiceResult<ServiceData.TodoItem>
            {
                Result = _mapper.Map<ServiceData.TodoItem>(createdDbTodoItem),
                IsSuccess = true,
            };
        }
    }
}