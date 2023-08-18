using Application.Interfaces;
using Application.TodoItems.Commands;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.CommandHandlers;

public class DeleteTodoItemHandler : IRequestHandler<DeleteTodoItem>
{
    private readonly ITodoItemsRepository _todoItemsRepository;

    public DeleteTodoItemHandler(ITodoItemsRepository todoItemsRepository)
    {
        _todoItemsRepository = todoItemsRepository;
    }
    
    public async Task Handle(DeleteTodoItem request, CancellationToken cancellationToken)
    {
        await _todoItemsRepository.DeleteTodoItem(request.Id);
    }
}