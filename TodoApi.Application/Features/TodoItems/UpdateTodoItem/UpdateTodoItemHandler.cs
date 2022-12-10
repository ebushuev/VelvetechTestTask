using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Database;

namespace TodoApiDTO.Application.Features.TodoItems.UpdateTodoItem;

public class UpdateTodoItemHandler : IRequestHandler<UpdateTodoItemQuery>
{
    private readonly TodoContext _context;

    public UpdateTodoItemHandler(TodoContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateTodoItemQuery request, 
        CancellationToken cancellationToken)
    {
        var todoItemToUpdate = await _context.TodoItems
            .AsNoTracking()
            .SingleOrDefaultAsync(item => item.Id == request.Item.Id, cancellationToken);
        
        if (todoItemToUpdate == null)
        {
            throw new ArgumentException($"TodoItem with id '{request.Item.Id}' does not exist");
        }

        todoItemToUpdate = todoItemToUpdate with
        {
            Name = request.Item.Name,
            IsComplete = request.Item.IsComplete
        };

        _context.Update(todoItemToUpdate);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}