using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
	public class TodoItemService : ITodoItemService
	{
		private readonly IUnitOfWork _unitOfWork;

		public TodoItemService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync()
		{
			var dbData = await _unitOfWork.TodoItemRepository.GetAllAsync();

			return dbData.Select(ItemToDTO);
		}

		public async Task<Response<TodoItemDTO>> GetTodoItemAsync(long id)
		{
			var exists = await _unitOfWork.TodoItemRepository.ExistAsync(x => x.Id == id);

			if (!exists)
				return new Response<TodoItemDTO>(null, ItemState.NotFound);

			var dbData = await _unitOfWork.TodoItemRepository.GetByIdAsync(id);

			return new Response<TodoItemDTO>(ItemToDTO(dbData), ItemState.Default);
		}

		public async Task<Response<TodoItemDTO>> AddTodoItemAsync(TodoItemDTO todoItemDTO)
		{
			if (todoItemDTO is null)
				return new Response<TodoItemDTO>(null, ItemState.Null);

			var todoItem = DTOToItem(todoItemDTO);

			await _unitOfWork.TodoItemRepository.AddAsync(todoItem);
			await _unitOfWork.SaveAsync();

			return new Response<TodoItemDTO>(ItemToDTO(todoItem), ItemState.Added);
		}

		public async Task<Response> DeleteTodoItemAsync(long id)
		{
			var exists = await _unitOfWork.TodoItemRepository.ExistAsync(x => x.Id == id);

			if (!exists)
				return new Response(ItemState.NotFound);

			var dbData = await _unitOfWork.TodoItemRepository.GetByIdAsync(id);

			await _unitOfWork.TodoItemRepository.DeleteAsync(dbData);
			await _unitOfWork.SaveAsync();

			return new Response(ItemState.Deleted);
		}

		public async Task<Response> UpdateTodoItemAsync(TodoItemDTO todoItemDTO)
		{
			var exists = await _unitOfWork.TodoItemRepository.ExistAsync(x => x.Id == todoItemDTO.Id);

			if (!exists)
				return new Response(ItemState.NotFound);

			var todoItem = await _unitOfWork.TodoItemRepository.GetByIdAsync(todoItemDTO.Id);

			todoItem.Name = todoItemDTO.Name;
			todoItem.IsComplete = todoItemDTO.IsComplete;

			await _unitOfWork.TodoItemRepository.UpdateAsync(todoItem);
			await _unitOfWork.SaveAsync();

			return new Response(ItemState.Updated);
		}

		#region Private
		private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
			new TodoItemDTO
			{
				Id = todoItem.Id,
				Name = todoItem.Name,
				IsComplete = todoItem.IsComplete
			};

		private static TodoItem DTOToItem(TodoItemDTO todoItemDTO) =>
			new TodoItem
			{
				Name = todoItemDTO.Name,
				IsComplete = todoItemDTO.IsComplete,
			};

		#endregion
	}
}