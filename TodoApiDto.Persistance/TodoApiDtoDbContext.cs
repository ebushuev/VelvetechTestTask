using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using TodoApiDto.Application.Interfaces;
using TodoApiDto.Domain.Entities;

namespace TodoApiDto.Persistance
{
    public class TodoApiDtoDbContext: DbContext, ITodoApiDtoDbContext
    {
        public TodoApiDtoDbContext(DbContextOptions<TodoApiDtoDbContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> Items { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
