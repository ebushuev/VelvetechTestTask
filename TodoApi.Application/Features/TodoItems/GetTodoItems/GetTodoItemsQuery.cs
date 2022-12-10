using MediatR;

namespace TodoApiDTO.Application.Features.TodoItems.GetTodoItems;

public record GetTodoItemsQuery(int PageNumber, int PageSize) : IRequest<GetTodoItemsResult>;