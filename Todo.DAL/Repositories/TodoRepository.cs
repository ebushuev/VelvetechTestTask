using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.DAL.Models;

namespace Todo.DAL.Repositories
{
    public class TodoRepository: BaseRepository<TodoItem>
    {
        public TodoRepository(TodoContext db) : base(db)
        {
        }
    }
}