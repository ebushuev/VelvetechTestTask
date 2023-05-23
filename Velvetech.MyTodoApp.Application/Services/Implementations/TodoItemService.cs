using AutoMapper;
using System.Linq.Expressions;
using Velvetech.MyTodoApp.Application.DTOs;
using Velvetech.MyTodoApp.Application.Exceptions;
using Velvetech.MyTodoApp.Application.Services.Abstractions;
using Velvetech.TodoApp.Domain.Entities;
using Velvetech.TodoApp.Infrastructure.Repositories.Abstractions.Custom;

namespace Velvetech.MyTodoApp.Application.Services.Implementations
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemReadDto>> GetTodoItemsAsync(Expression<Func<TodoItemEntity, bool>> predicate)
        {
            return _mapper.Map<IEnumerable<TodoItemReadDto>>(await _repository.GetAsync(predicate));
        }

        public async Task<IEnumerable<TodoItemReadDto>> GetTodoItemsAsync()
        {
            return _mapper.Map<IEnumerable<TodoItemReadDto>>(await _repository.GetAsync());
        }

        public async Task<TodoItemReadDto> GetTodoItemAsync(Expression<Func<TodoItemEntity, bool>> predicate)
        {
            TodoItemEntity entity = await _repository.GetFirstOrDefaultAsync(predicate);
            if (entity is null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<TodoItemReadDto>(entity);
        }

        public async Task<TodoItemReadDto> AddTodoItemAsync(TodoItemCreateDto todoItemDto)
        {
            TodoItemEntity todoItemEntity = _mapper.Map<TodoItemEntity>(todoItemDto);

            return _mapper.Map<TodoItemReadDto>(await _repository.AddAsync(todoItemEntity));
        }

        public async Task<TodoItemReadDto> UpdateTodoItemAsync(TodoItemUpdateDto todoItemDto)
        {
            await ValidateId(todoItemDto.Id);

            TodoItemEntity todoItemEntity = _mapper.Map<TodoItemEntity>(todoItemDto);

            return _mapper.Map<TodoItemReadDto>(await _repository.UpdateAsync(todoItemEntity));
        }

        public async Task<bool> DeleteTodoItemAsync(Guid id)
        {
            await ValidateId(id);

            TodoItemEntity todoItemEntity = await _repository.GetFirstOrDefaultAsync(o => o.Id == id);

            return await _repository.DeleteAsync(todoItemEntity);
        }

        private async Task ValidateId(Guid id)
        {
            if (!await _repository.AnyAsync(o => o.Id == id))
            {
                throw new EntityNotFoundException();
            }
        }
    }
}
