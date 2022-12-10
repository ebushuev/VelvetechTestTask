using MediatR;
using TodoApi.Core;

namespace TodoApiDTO.Application.Features.TodoItems.CreateTodoItem;

public record CreateTodoItemQuery(string Name) : IRequest<TodoItem>;