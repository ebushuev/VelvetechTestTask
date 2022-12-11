using MediatR;
using TodoApi.Core;
using TodoApi.Database;
using TodoApi.Database.Models;
using TodoApiDTO.Application.Features.Common.Mapping;

namespace TodoApiDTO.Application.Features.TodoItems.CreateTodoItem;

public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemQuery, TodoItem>
{
    private readonly TodoContext _context;

    public CreateTodoItemHandler(TodoContext context)
    {
        _context = context;
    }

    public async Task<TodoItem> Handle(CreateTodoItemQuery request, CancellationToken cancellationToken)
    {
        var dbo = new TodoItemDbo(0, request.Name, false, null);

        _context.TodoItems.Add(dbo);
        await _context.SaveChangesAsync(cancellationToken);

        return dbo.MapToModel();
    }
}