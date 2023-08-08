using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.BLL.DTO;
using TodoApi.BLL.Interfaces;
using TodoApi.DAL.EF;
using TodoApi.DAL.Entities;
using TodoApi.DAL.Interfaces;

namespace TodoApi.BLL
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TodoItemDTO>> GetAllAsync()
        {
            return await _unitOfWork.TodoRepository.GetAll()
                .Select(x => ItemToDTO(x)).ToListAsync();
        }

        public async Task<TodoItemDTO> GetAsync(int id)
        {
            var todoItem = await _unitOfWork.TodoRepository.GetAsync(id);

            return todoItem == null? null : ItemToDTO(todoItem);
        }


        public async Task<TodoItem> AddAsync(TodoItemDTO todoItemDTO)
        {
            if (todoItemDTO.Id != 0 || string.IsNullOrWhiteSpace(todoItemDTO.Name))
            {
                return null;
            }

            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _unitOfWork.TodoRepository.Add(todoItem);
            await _unitOfWork.SaveAsync();
            return todoItem;
        }

        public async Task<bool> UpdateAsync(TodoItemDTO todoItemDTO)
        {
            if (string.IsNullOrWhiteSpace(todoItemDTO.Name))
            {
                return false;
            }

            var todoItem = new TodoItem
            {
                Id = todoItemDTO.Id,
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            bool isUpdated = await _unitOfWork.TodoRepository.UpdateAsync(todoItem);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(todoItemDTO.Id))
            {
                return false;
            }

            return isUpdated;
        }

        private bool TodoItemExists(long id) =>
            _unitOfWork.TodoRepository.GetAll().Any(e => e.Id == id);

        public async Task<bool> RemoveAsync(int id)
        {
            bool isDeleted = await _unitOfWork.TodoRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();

            return isDeleted;
        }

        public static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
