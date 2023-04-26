using TodoCore.Data.Entities;
using TodoCore.Data.Interfaces;

namespace TodoInfrastructure.DataAccess.Repositories
{
    public class TodoItemReposytory : Reposytory<TodoItem>, ITodoItemReposytory
    {
        public TodoItemReposytory(ApplicationDbContext context) : base(context) { }
    }
}
