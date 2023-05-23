using DataAccessLayer.Repository.Abstractions.Base;
using Velvetech.TodoApp.Domain.Entities;

namespace Velvetech.TodoApp.Infrastructure.Repositories.Abstractions.Custom
{
    public interface ITodoItemRepository : IBaseRepository<TodoItemEntity>
    {

    }
}
