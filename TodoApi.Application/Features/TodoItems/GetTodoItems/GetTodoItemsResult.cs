using System.Collections.Immutable;
using TodoApi.Core;

namespace TodoApiDTO.Application.Features.TodoItems.GetTodoItems;

public record GetTodoItemsResult(ImmutableArray<TodoItem> Items);