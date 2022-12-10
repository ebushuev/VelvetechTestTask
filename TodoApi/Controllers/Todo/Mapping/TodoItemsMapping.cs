using System.Collections.Immutable;
using System.Linq;
using TodoApi.Controllers.Todo.Models;
using TodoApi.Core;
using TodoApiDTO.Application.Features.TodoItems.CreateTodoItem;
using TodoApiDTO.Application.Features.TodoItems.GetTodoItems;

namespace TodoApi.Controllers.Todo.Mapping;

internal static class TodoItemsMapping
{
    internal static GetTodoItemsResponse MapToResponse(this GetTodoItemsResult model)
    {
        return new GetTodoItemsResponse(model.Items
            .Select(item => item.MapToDto())
            .ToImmutableArray());
    }

    internal static TodoItemDto MapToDto(this TodoItem todoItem)
    {
        return new TodoItemDto(
            todoItem.Id,
            todoItem.Name,
            todoItem.IsComplete);
    }

    internal static CreateTodoItemQuery MapToQuery(this CreateTodoItemRequest request)
    {
        return new CreateTodoItemQuery(request.Name);
    }

    internal static TodoItem MapToModel(this TodoItemDto dto)
    {
        return new TodoItem(
            dto.Id,
            dto.Name,
            dto.IsComplete);
    }
}