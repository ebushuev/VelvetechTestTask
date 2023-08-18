using Application.Interfaces;
using Application.TodoItems.Queries;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.QueryHandlers;

public class GetTodoItemByIdHandler : IRequestHandler<GetTodoItemById, TodoItem>
{
    private readonly ITodoItemsRepository _todoItemsRepository;

    public GetTodoItemByIdHandler(ITodoItemsRepository todoItemsRepository)
    {
        _todoItemsRepository = todoItemsRepository;
    }
    
    public async Task<TodoItem> Handle(GetTodoItemById request, CancellationToken cancellationToken)
    {
        return await _todoItemsRepository.GetById(request.Id);
    }
}