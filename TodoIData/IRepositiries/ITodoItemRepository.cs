using TodoEntities.DbSet;

namespace TodoIData.IRepositiries
{
    public interface ITodoItemRepository : IGenericRepository<TodoItem>
    {
    }
}
