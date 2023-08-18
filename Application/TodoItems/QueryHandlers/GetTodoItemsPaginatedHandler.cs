using Application.Interfaces;
using Application.TodoItems.Queries;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.QueryHandlers;

public class GetTodoItemsPaginatedHandler : IRequestHandler<GetTodoItemsPaginated, IEnumerable<TodoItem>>
{
    private readonly ITodoItemsRepository _todoItemsRepository;

    public GetTodoItemsPaginatedHandler(ITodoItemsRepository todoItemsRepository)
    {
        _todoItemsRepository = todoItemsRepository;
    }
    
    public async Task<IEnumerable<TodoItem>> Handle(GetTodoItemsPaginated request, CancellationToken cancellationToken)
    {
        if (request.Page < 1 || request.PageSize < 1)
        {
            throw new ArgumentException("Page and Page Size cannot be less than 1");
        }
        
        return await _todoItemsRepository.GetAllPaginated(request.Page, request.PageSize);
    }
}