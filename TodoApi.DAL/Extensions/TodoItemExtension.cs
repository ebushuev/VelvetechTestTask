using TodoApi.DTO;
using TodoApi.DAL.Entities;

namespace TodoApi.DAL.Extensions
{
    public static class TodoItemExtension
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
