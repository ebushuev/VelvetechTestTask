using Domain;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.ToDo.GetToDoItems
{
    public class GetTodoItemsQuery : IRequest<ICollection<TodoItem>>
    {
    }
}
