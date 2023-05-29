using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TodoApiDto.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TodoApiDto.Application.Interfaces
{
    public interface ITodoApiDtoDbContext
    {
        DbSet<TodoItem> Items { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
