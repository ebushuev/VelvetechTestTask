using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApi.DataLayer.DataAccess
{
    public interface IEntityAccessService<TEntity> where TEntity : class
    {
        ValueTask<TEntity> Find(params object[] keyValues);

        ValueTask<List<TEntity>> GetAll();
    }
}