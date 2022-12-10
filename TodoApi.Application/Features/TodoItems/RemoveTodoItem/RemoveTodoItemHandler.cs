using MediatR;
using TodoApi.Database;

namespace TodoApiDTO.Application.Features.TodoItems.RemoveTodoItem;

public class RemoveTodoItemHandler : IRequestHandler<RemoveTodoItemQuery>
{
    private readonly TodoContext _context;

    public RemoveTodoItemHandler(TodoContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RemoveTodoItemQuery request, CancellationToken cancellationToken)
    {
        var todoItem = await _context.TodoItems.FindAsync(request.Id);

        if (todoItem == null)
        {
            throw new ArgumentException($"TodoItem with id '{request.Id}' does not exist");
        }

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}