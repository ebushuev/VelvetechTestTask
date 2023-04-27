using TodoApi.DataAccessLayer.Abstract;
using TodoApi.DataAccessLayer.Context;
using TodoApi.DataAccessLayer.Repositories;
using TodoApi.EntityLayer.Entities;

namespace TodoApi.DataAccessLayer.EntityFramework
{
    public class EFTodoItemRepository : GenericRepository<TodoItem>, ITodoItemDal
    {
        public EFTodoItemRepository(TodoContext context) : base(context)
        {
        }
    }
}
