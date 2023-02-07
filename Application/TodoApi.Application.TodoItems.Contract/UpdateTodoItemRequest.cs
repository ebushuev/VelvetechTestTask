using MediatR;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems.Contract
{
    public class UpdateTodoItemRequest: IRequest
    {
        public long Id { get; set; }
        
        public TodoItem TodoItem { get; set; }
    }
}