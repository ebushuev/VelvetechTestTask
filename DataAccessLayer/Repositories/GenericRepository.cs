using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DataAccessLayer.Abstract;
using TodoApi.DataAccessLayer.Context;
using TodoApi.EntityLayer.Entities.Abstract;

namespace TodoApi.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : BaseEntity
    {
        protected private TodoContext _context;

        public GenericRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task DeleteAsync(long id)
        {
            var item = await _context.Set<T>().FindAsync(id)
                ?? throw new NullReferenceException("The item with the provided 'id' doesn't exist in the db.");
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id) ?? throw new NullReferenceException("The item with the provided 'id' doesn't exist in the db.");
        }
        public async Task UpdateAsync(long id, T item)
        {
            if (id != item.Id)
            {
                throw new ArgumentException("The 'id' parameter doesn't match the 'Id' property of the 'item'.");
            }
            if (!ItemExists(_context, id))
                throw new NullReferenceException("The item with the provided 'id' doesn't exist in the db.");
            _context.Update(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ItemExists(_context, id))
            {
                throw new InvalidOperationException("An error occurred while attempting to modify the item with the 'id' in the db. " +
                    "The item no longer exists in the database.");
            }
        }

        private static bool ItemExists(DbContext context, long id) =>
           context.Set<T>().Any(e => e.Id == id);
    }
}
