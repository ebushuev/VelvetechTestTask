using MediatR;
using TodoApi.Core;
using TodoApi.Database;
using TodoApiDTO.Application.Features.Common.Mapping;

namespace TodoApiDTO.Application.Features.TodoItems.GetTodoItem;

public class GetTodoItemHandler : IRequestHandler<GetTodoItemQuery, TodoItem>
{
    private readonly TodoContext _context;

    public GetTodoItemHandler(TodoContext context)
    {
        _context = context;
    }

    public async Task<TodoItem> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {
        var dbo = await _context.TodoItems.FindAsync(request.Id, cancellationToken);

        if (dbo == null)
        {
            throw new ArgumentException($"TodoItem with id '{request.Id}' does not exist");
        }

        return dbo.MapToModel();
    }
}