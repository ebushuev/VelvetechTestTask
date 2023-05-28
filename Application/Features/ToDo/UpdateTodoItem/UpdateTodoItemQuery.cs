using Domain;
using MediatR;

namespace Application.Features.ToDo.UpdateTodoItem
{
    public class UpdateTodoItemQuery: IRequest
    {
        public long Id { get; set; } 
        public TodoItem TodoItem { get; set; }
    }
}
