using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Todo.Common.Commands;
using Todo.Common.Dto;
using Todo.DAL;
using Todo.DAL.Models;

namespace Todo.BLL.CommandHandlers
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoItemDto>
    {
        private readonly TodoContext _context;

        public CreateTodoCommandHandler(TodoContext todoContext)
        {
            _context = todoContext;
        }

        public async Task<TodoItemDto> Handle(CreateTodoCommand request, CancellationToken ct)
        {
            var todoItem = request.Item.Adapt<TodoItemEntity>();

            todoItem.Id = default;

            _context.Set<TodoItemEntity>().Add(todoItem);

            await _context.SaveChangesAsync(ct);

            return todoItem.Adapt<TodoItemDto>();
        }
    }
}
