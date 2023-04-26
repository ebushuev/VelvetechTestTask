using AutoMapper;
using System;
using System.Threading.Tasks;
using TodoCore.Data.Entities;
using TodoCore.Data.Interfaces;
using TodoCore.DTOs;
using TodoCore.Exceptions;
using TodoCore.Services;

namespace TodoApplication.Services
{
    public class DeleteTodoItemService : IDeleteTodoItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTodoItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TodoItemDTO> DeleteTodoItemAsync(long id)
        {
            TodoItem todoItem;
            try
            {
                todoItem = await _unitOfWork.TodoItemReposytory.GetByIdAsync(id);
            }
            catch(EntityNotFoundException<TodoItem> ex)
            {
                throw ex;
            }
            using var transaction = _unitOfWork.StartTransation();
            try
            {
                _unitOfWork.TodoItemReposytory.Delete(todoItem);
                await _unitOfWork.SaveChangesAsync();
                transaction.Commit();
                return _mapper.Map<TodoItemDTO>(todoItem);
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw new SomethingWentWrongException("Something went wrong while deleting todo item");
            }
        }
    }
}
