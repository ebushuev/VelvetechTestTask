using MediatR;
using Todo.Core.Business.TodoItem.Dto;

namespace Todo.Core.Business.TodoItem.Queries
{
    public class GetAllQuery: IRequest<TodoItemDto[]>
    {
    }
}
