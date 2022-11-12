using MediatR;

namespace Application.Todo.Queries.GetList
{
    public class GetTodoListQuery: IRequest<TodoDetailsListVm>
    {
    }
}
