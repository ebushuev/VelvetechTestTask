using TodoApi.Application.TodoItemBoundary.Models;
using TodoApi.Infrastructure.Data.Models;

namespace TodoApi.Application.TodoItemBoundary.Extensions
{
    public static class TodoItemExtensions
    {
        public static TodoItemDTO ItemToDTO(this TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
