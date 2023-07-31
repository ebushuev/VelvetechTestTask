using Todo.Core.Common;

namespace Todo.Core.Business.TodoItem.Interfaces
{
    public interface ITodoRepository : IRepository<Entities.TodoItem, Guid>
    {
    }
}
