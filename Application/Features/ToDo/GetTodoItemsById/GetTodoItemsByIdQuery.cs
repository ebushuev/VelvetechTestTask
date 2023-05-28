using Domain;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.ToDo.GetToDoItemsById
{
    public class GetTodoItemsByIdQuery : IRequest<TodoItem>
    {
        public long TodoItemId { get; set; }
    }
}
