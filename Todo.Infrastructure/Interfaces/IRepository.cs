using System.Linq.Expressions;

namespace Todo.Infrastructure.Interfaces;

public interface IRepository<T>
    where T : class
{
    Task Create(T entity);

    Task<T> Read(Func<T, bool> condition);

    Task Update(T entity);

    Task Delete(T entity);

    Task SaveChanges();

    Task<IEnumerable<T>> GetAll();
}