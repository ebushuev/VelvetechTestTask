using MediatR;

namespace TodoApiDTO.Application.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest
    {
        public long Id { get; set; }
    }
}
