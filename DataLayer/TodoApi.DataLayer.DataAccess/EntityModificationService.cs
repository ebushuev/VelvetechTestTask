using System.Threading.Tasks;
using TodoApi.Infrastructure.DbContext;

namespace TodoApi.DataLayer.DataAccess
{
    public class EntityModificationService<TEntity> : IEntityModificationService<TEntity>
        where TEntity : class
    {
        private readonly TodoContext _context;

        public EntityModificationService(TodoContext context)
        {
            _context = context;
        }

        public TEntity Update(TEntity entity)
        {
            return _context.Update(entity).Entity;
        }

        public async ValueTask<TEntity> Create(TEntity entity)
        {
            var result = await _context.AddAsync(entity);
            
            return result.Entity;
        }

        public void Remove(TEntity entity)
        {
            _context.Remove(entity);
        }
    }
}