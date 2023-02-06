using System.Threading;
using System.Threading.Tasks;
using TodoApi.Infrastructure.DbContext;

namespace TodoApi.DataLayer.DataAccess
{
    public class EntityAccessService<TEntity> : IEntityAccessService<TEntity>
        where TEntity : class
    {
        private readonly TodoContext _context;

        public EntityAccessService(TodoContext context)
        {
            _context = context;
        }

        public ValueTask<TEntity> Find(params object[] keyValues)
        {
            return _context.FindAsync<TEntity>(keyValues);
        }
    }
}