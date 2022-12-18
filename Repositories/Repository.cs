using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.Repositories
{
    public class Repository<T, U> : IRepository<T> where U : DbContext
                                                   where T : class
    {
        private readonly U _context;

        public Repository(U context) 
        {
            _context = context;
        }

        public async Task<T> Create(T item)
        {
            _context.Set<T>().Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Remove(long id)
        {
            var item = await _context.Set<T>().FindAsync(id);

            if (item == null)
            {
                return false;
            }

            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<T> FindAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> List()
        {
            return _context.Set<T>();
        }

        public async Task<bool> Update(T item)
        {            
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}
