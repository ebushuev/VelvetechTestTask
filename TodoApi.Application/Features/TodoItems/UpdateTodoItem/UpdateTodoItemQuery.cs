using MediatR;
using TodoApi.Core;

namespace TodoApiDTO.Application.Features.TodoItems.UpdateTodoItem;

public record UpdateTodoItemQuery(
    TodoItem Item) : IRequest;