using System;
using System.Threading.Tasks;
using TodoCore.Data.Entities;
using TodoCore.Data.Interfaces;
using TodoCore.DTOs;
using TodoCore.Exceptions;
using TodoCore.Services;

namespace TodoApplication.Services
{
    public class UpdateTodoItemService : IUpdateTodoItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTodoItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TodoItemDTO> UpdateTodoItemAsync(TodoItemDTO itemDTO)
        {
            TodoItem todoItem;
            try
            {
                todoItem = await _unitOfWork.TodoItemReposytory.GetByIdAsync(itemDTO.Id);
            }
            catch(EntityNotFoundException<TodoItem> ex)
            {
                throw ex;
            }
            todoItem.Name = itemDTO.Name;
            todoItem.IsComplete = itemDTO.IsComplete;
            using var transaction = _unitOfWork.StartTransation();
            try
            {
                _unitOfWork.TodoItemReposytory.Update(todoItem);
                await _unitOfWork.SaveChangesAsync();
                transaction.Commit();
                return itemDTO;
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw new SomethingWentWrongException("Something went wrong wile updating item");
            }
            
        }
    }
}
