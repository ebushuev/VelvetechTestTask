using MediatR;

namespace TodoApiDTO.Application.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : TodoItemCommand, IRequest<long>
    {
    }
}
