using TodoApi.DAL.Models;
using TodoApi.Database;
using ToDoItems.DAL.Interfaces;

namespace TodoApiDTO.ToDoApi.DAL.Repositories.Implementation
{
    /// <summary>
    /// ToDoItems repository
    /// </summary>
    public class ToDoItemsRepository : BaseRepository<TodoItem>, IRepository <TodoItem>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">database context</param>
        public ToDoItemsRepository(ToDoContext dbContext):base(dbContext)
        {

        }
    }
}
