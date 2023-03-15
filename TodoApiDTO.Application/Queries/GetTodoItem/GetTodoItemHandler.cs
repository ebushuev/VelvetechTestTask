using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoApiDTO.Infrastructure.EfCore;

namespace TodoApiDTO.Application.Queries.GetTodoItem
{
    public class GetTodoItemHandler : IRequestHandler<GetTodoItemQuery, TodoItemViewModel>
    {
        private readonly TodoApiDTOContext _db;

        public GetTodoItemHandler(TodoApiDTOContext db)
        {
            _db = db;
        }

        public async Task<TodoItemViewModel> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
        {
            var item = await _db.TodoItems.FindAsync(request.Id);

            if (item == null)
            {
                throw new NotFoundException();
            }

            return TodoItemViewModel.MapFrom(item);
        }
    }
}
