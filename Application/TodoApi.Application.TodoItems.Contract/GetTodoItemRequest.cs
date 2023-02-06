using MediatR;

namespace TodoApi.Application.TodoItems.Contract
{
    public class GetTodoItemRequest: IRequest<DataLayer.Entity.TodoItem>
    {
        public long Id { get; set; }
    }
}