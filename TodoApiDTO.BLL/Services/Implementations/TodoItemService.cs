using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.BLL.DTOs.TodoItems;
using TodoApiDTO.BLL.Services.Abstractions;
using TodoApiDTO.DAL.Contexts;
using TodoApiDTO.DAL.Entities;
using TodoApiDTO.DAL.UnitOfWork;
using TodoApiDTO.Shared.Exceptions;

namespace TodoApiDTO.BLL.Services.Implementations
{
    public class TodoItemService : BaseService<TodoItem>, ITodoItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TodoItemService(
            TodoDbContext dbContext,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateTodoItemResponseDTO> AddAsync(CreateTodoItemRequestDTO requestDTO)
        {
            var todoItemEntity = _mapper.Map<TodoItem>(requestDTO);

            await _unitOfWork.TodoItemRepository.AddAsync(todoItemEntity);

            await _unitOfWork.SaveChangesAsync();

            var todoItemResponseDTO = _mapper.Map<CreateTodoItemResponseDTO>(todoItemEntity);

            return todoItemResponseDTO;
        }

        public async Task DeleteAsync(long id)
        {
            var todoItemEntity = await _unitOfWork.TodoItemRepository.GetByIdAsync(id);

            if (todoItemEntity == null)
                throw new TodoItemNotFoundException("Todo item not found");

            _unitOfWork.TodoItemRepository.Delete(todoItemEntity);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoItemResponseDTO>> GetAllAsync()
        {
            var todoItemEntities = await _unitOfWork.TodoItemRepository.GetAllAsync();

            var todoItemResponseDTOs = _mapper.Map<IEnumerable<TodoItemResponseDTO>>(todoItemEntities);

            return todoItemResponseDTOs;
        }

        public async Task<TodoItemResponseDTO> GetTodoItemByIdAsync(long id)
        {
            var todoItemEntity = await _unitOfWork.TodoItemRepository.GetByIdAsync(id);

            if (todoItemEntity == null)
                throw new TodoItemNotFoundException("Todo item not found");

            var todoItemResponseDTO = _mapper.Map<TodoItemResponseDTO>(todoItemEntity);

            return todoItemResponseDTO;
        }

        public async Task UpdateAsync(long id, UpdateTodoItemRequestDTO requestDTO)
        {
            if (id != requestDTO.Id)
                throw new BadRequestException("Bad Request");

            var todoItemEntity = await _unitOfWork.TodoItemRepository.GetByIdAsync(id);

            if (todoItemEntity == null)
                throw new TodoItemNotFoundException("Todo item not found");

            _mapper.Map(requestDTO, todoItemEntity);

            _unitOfWork.TodoItemRepository.Update(todoItemEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                throw new TodoItemNotFoundException("Todo item not found");
            }
        }

        private bool TodoItemExists(long id) 
            => _unitOfWork.TodoItemRepository.TodoItemExists(id);
    }
}
