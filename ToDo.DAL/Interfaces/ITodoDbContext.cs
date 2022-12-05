using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Models;

namespace ToDo.DAL.Interfaces
{
    public interface ITodoDbContext
    {
        public DbSet<ToDoItem> TodoItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}