using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Data.Interfaces;

namespace TodoApi.Data
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly TodoContext _context;
        private readonly DbSet<TEntity> _table = null;

        public Repository(TodoContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAsync()
        {
            return await _table.ToArrayAsync();
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await _table.FindAsync(id);
        }

        public void Update(TKey id, TEntity item)
        {
            _table.Attach(item);
            _context.Update(item);
        }

        public async Task CreateAsync(TEntity item)
        {
            await _context.AddAsync(item);
        }

        public async Task DeleteAsync(TKey id)
        {
            TEntity existing = await GetAsync(id);
            _table.Remove(existing);
        }

        /// <summary>
        /// Takes from https://docs.microsoft.com/en-us/ef/ef6/saving/concurrency how to deal with 
        /// concurrency while saving. Add only check that if concurrecy exception - entity should exists in database.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public async Task SaveAsync()
        {
            bool saveFailed;
            do
            {
                saveFailed = false;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update original values from the database
                    foreach (var entry in ex.Entries)
                    {
                        var databaseValues = entry.GetDatabaseValues();
                        if (databaseValues == null)
                        {
                            throw ex;
                        }

                        entry.OriginalValues.SetValues(databaseValues);
                    }
                }

            } while (saveFailed);
        }
    }
}
