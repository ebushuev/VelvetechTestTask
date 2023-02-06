using System.Threading;
using System.Threading.Tasks;
using TodoApi.Infrastructure.DbContext;

namespace TodoApi.DataLayer.DataAccess
{
    public class EntityAccessService<TEntity> 
        where TEntity : class
    {
        private readonly TodoContext _context;

        public EntityAccessService(TodoContext context)
        {
            _context = context;
        }

        public ValueTask<TEntity> Find(object keyValues, CancellationToken cancellationToken)
        {
            return _context.FindAsync<TEntity>(keyValues, cancellationToken);
        }
    }
}