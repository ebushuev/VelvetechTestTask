using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Database;
using TodoApiDTO.Application.Features.TodoItems.GetTodoItems.Mapping;

namespace TodoApiDTO.Application.Features.TodoItems.GetTodoItems;

public class GetTodoItemsHandler : IRequestHandler<GetTodoItemsQuery, GetTodoItemsResult>
{
    private readonly TodoContext _context;

    public GetTodoItemsHandler(TodoContext context)
    {
        _context = context;
    }

    public async Task<GetTodoItemsResult> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var dbos = await _context.TodoItems
            .OrderBy(item => item.Id)
            .Skip(request.PageNumber * request.PageSize)
            .Take(request.PageSize)
            .ToArrayAsync(cancellationToken);

        return dbos.MapToModel();
    }
}