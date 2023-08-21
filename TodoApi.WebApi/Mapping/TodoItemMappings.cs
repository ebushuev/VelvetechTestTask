namespace TodoApi.WebApi.Mapping
{
    using TodoApi.ObjectModel.Models;
    using TodoApi.WebApi.Dto;

    internal static class TodoItemMappings
    {
        internal static TodoItemDto ItemToDto(TodoItem todoItem) =>
            new TodoItemDto
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

        internal static TodoItem DtoToItem(BaseTodoItemDto todoItemDto) =>
            new TodoItem
            {
                Name = todoItemDto.Name,
                IsComplete = todoItemDto.IsComplete
            };
    }
}