using System.Linq.Expressions;

namespace Todo.Infrastructure.Interfaces;

public interface IRepository<T>
    where T : class
{
    void Create(T entity);

    Task<T> Read(Expression<Func<T, bool>> condition);

    void Update(T entity);

    void Delete(T entity);

    Task SaveChanges();

    Task<IEnumerable<T>> GetAll();
}