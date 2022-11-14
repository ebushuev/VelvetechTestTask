using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Models;

namespace TodoApi.Interfaces.DataAccess
{
    public interface ITodoDbContext
    {
        DbSet<TodoItem> TodoItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
