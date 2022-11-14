using System.Linq;
using TodoApi.Domain.Models;

namespace TodoApi.Interfaces.BusinessRules
{
    public interface ITodoItemBusinessRules
    {
        TodoItem GetById (IQueryable<TodoItem> query, long id);
    }
}
