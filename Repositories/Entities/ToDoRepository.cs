using TodoApi.Models;
using TodoApiDTO.Repositories.Entities;

namespace TodoApiDTO.Repositories.ToDo
{
    public class ToDoRepository : RepositoryBase<TodoItem>, IToDoRepository
    {
        public ToDoRepository(TodoContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
