using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCore.Data.Common;
using TodoCore.Data.Interfaces;
using TodoCore.Exceptions;

namespace TodoInfrastructure.DataAccess.Repositories
{
    public class Reposytory<T> : IReposytory<T>
        where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public Reposytory(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }

        public Task<List<T>> GetAllAsync()
        {
            return _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            var entity = await _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                throw new EntityNotFoundException<T>("entity with id {id} wasn't found");
            return entity;
        }

        public async Task<bool> IsExistAsync(long id)
        {
            var entity = await _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
            return entity != null;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
    }
}
