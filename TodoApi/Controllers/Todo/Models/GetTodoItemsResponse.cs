using System.Collections.Immutable;

namespace TodoApi.Controllers.Todo.Models;

public record GetTodoItemsResponse(
    ImmutableArray<TodoItemDto> Items);