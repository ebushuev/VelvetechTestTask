using Todo.Infrastructure.Interfaces;

namespace Todo.Infrastructure.Repositories;

public class Repository<T> : IRepository<T>
    where T : class
{
    public async Task Create(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T> Read(Func<T, bool> condition)
    {
        throw new NotImplementedException();
    }

    public async Task Update(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
    }
}