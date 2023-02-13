using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common;
using Domain.DTOs;

namespace Domain.Services
{
	public interface ITodoItemService
	{
		Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync();
		Task<Response<TodoItemDTO>> GetTodoItemAsync(long id);
		Task<Response<TodoItemDTO>> AddTodoItemAsync(TodoItemDTO todoItemDTO);
		Task<Response> DeleteTodoItemAsync(long id);
		Task<Response> UpdateTodoItemAsync(TodoItemDTO todoItemDTO);
	}
}
