using Application.Interfaces;
using Application.TodoItems.Queries;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.QueryHandlers;

public class GetAllTodoItemsHandler : IRequestHandler<GetAllTodoItems, IEnumerable<TodoItem>>
{
    private readonly ITodoItemsRepository _todoItemsRepository;

    public GetAllTodoItemsHandler(ITodoItemsRepository todoItemsRepository)
    {
        _todoItemsRepository = todoItemsRepository;
    }
    
    public async Task<IEnumerable<TodoItem>> Handle(GetAllTodoItems request, CancellationToken cancellationToken)
    {
        return await _todoItemsRepository.GetAll();
    }
}