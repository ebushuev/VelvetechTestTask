using MediatR;

namespace TodoApi.Application.TodoItems.Contract
{
    public class DeleteTodoItemRequest: IRequest
    {
        public long Id { get; set; }
    }
}