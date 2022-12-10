using MediatR;

namespace TodoApiDTO.Application.Features.TodoItems.RemoveTodoItem;

public record RemoveTodoItemQuery(long Id) : IRequest;