using System.Linq.Expressions;

namespace Todo.DAL;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(bool trackChanges);

    Task<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

    Task SaveChanges();

    void Create(T entity);

    void Update(T entity);

    void Delete(T entity);
}
