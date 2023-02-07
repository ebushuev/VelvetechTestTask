using System.Threading.Tasks;

namespace TodoApi.DataLayer.DataAccess
{
    public interface IEntityModificationService<TEntity> where TEntity : class
    {
        TEntity Update(TEntity entity);

        ValueTask<TEntity> Create(TEntity entity);

        void Remove(TEntity entity);
    }
}