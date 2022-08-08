using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain.Models;

namespace Todo.Domain.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoItemModel> Get(long id);
        IEnumerable<TodoItemModel> GetAll();
        Task<long> Create(TodoItemModel model);
        Task Update(TodoItemModel model);
        Task Delete(long id);
    }
}
