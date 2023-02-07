using MediatR;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems.Contract
{
    public class CreateTodoItemRequest : IRequest<TodoItem>
    {
        public TodoItem TodoItem { get; set; }
    }
}