using Application.Interfaces;
using Application.TodoItems.Commands;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.CommandHandlers;

public class CreateTodoItemHandler : IRequestHandler<CreateTodoItem, TodoItem>
{
    private readonly ITodoItemsRepository _todoItemsRepository;

    public CreateTodoItemHandler(ITodoItemsRepository todoItemsRepository)
    {
        _todoItemsRepository = todoItemsRepository;
    }
    
    public async Task<TodoItem> Handle(CreateTodoItem request, CancellationToken cancellationToken)
    {
        var newTodoItem = new TodoItem
        {
            Name = request.Name,
            IsComplete = request.IsComplete,
            Secret = request.Secret
        };

        return await _todoItemsRepository.CreateTodoItem(newTodoItem);
    }
}