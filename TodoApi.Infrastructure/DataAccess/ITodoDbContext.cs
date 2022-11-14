using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Models;

namespace TodoApi.Infrastructure.DataAccess
{
    public interface ITodoDbContext
    {
        DbSet<TodoItem> TodoItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
