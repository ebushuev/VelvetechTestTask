using Microsoft.EntityFrameworkCore;
using TodoApiDTO.BLL.Services.Abstractions;
using TodoApiDTO.DAL.Contexts;
using TodoApiDTO.DAL.Entities.Abstractions;

namespace TodoApiDTO.BLL.Services.Implementations
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> _dbSet;

        public BaseService(TodoDbContext dbContext) => _dbSet = dbContext.Set<TEntity>();
    }
}
