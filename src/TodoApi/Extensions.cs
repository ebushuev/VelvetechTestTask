using TodoApi.Models;

namespace TodoApi
{
    public static class Extensions
    {
        public static TodoItemDTO AsDto(this IEntity todoItem)
        {
            return new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
        }

        public static TodoItem AsEntity(this TodoItemDTO todoItemDto)
        {
            return new TodoItem
            {
                Id = todoItemDto.Id,
                Name = todoItemDto.Name,
                IsComplete = todoItemDto.IsComplete
            };
        }
    }
}