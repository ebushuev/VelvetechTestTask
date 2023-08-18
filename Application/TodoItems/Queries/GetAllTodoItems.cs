using Domain.Entities;
using MediatR;

namespace Application.TodoItems.Queries;

public class GetAllTodoItems : IRequest<IEnumerable<TodoItem>>
{
    
}