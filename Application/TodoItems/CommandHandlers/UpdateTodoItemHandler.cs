using Application.Interfaces;
using Application.TodoItems.Commands;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.CommandHandlers;

public class UpdateTodoItemHandler : IRequestHandler<UpdateTodoItem, TodoItem>
{
    private readonly ITodoItemsRepository _todoItemsRepository;

    public UpdateTodoItemHandler(ITodoItemsRepository todoItemsRepository)
    {
        _todoItemsRepository = todoItemsRepository;
    }
    
    public async Task<TodoItem> Handle(UpdateTodoItem request, CancellationToken cancellationToken)
    {
        var updatedTodoItem = new TodoItem
        {
            Name = request.Name,
            IsComplete = request.IsComplete
        };
        return await _todoItemsRepository.UpdateTodoItem(request.Id, updatedTodoItem);
    }
}