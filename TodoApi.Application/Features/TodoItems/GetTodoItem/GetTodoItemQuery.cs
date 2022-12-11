using MediatR;
using TodoApi.Core;

namespace TodoApiDTO.Application.Features.TodoItems.GetTodoItem;

public record GetTodoItemQuery(long Id) : IRequest<TodoItem>;