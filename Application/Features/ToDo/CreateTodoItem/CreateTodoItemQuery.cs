
using Domain;
using MediatR;

namespace Application.Features.ToDo.CreateTodoItem
{
    public class CreateTodoItemQuery : IRequest<TodoItem>
    {
        public TodoItem TodoItem { get; set; }
    }
}
