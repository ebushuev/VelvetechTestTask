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
        private bool _disposed = false;

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

        public void Update(TEntity item)
        {
            _table.Attach(item);
            _context.Update(item);
        }

        public void Create(TEntity item)
        {
            _context.Add(item);
        }

        public void Delete(TEntity item)
        {
            if (_context.Entry(item).State == EntityState.Detached)
            {
                _table.Attach(item);
            }
            _table.Remove(item);
        }

        /// <summary>
        /// Takes from https://docs.microsoft.com/en-us/ef/ef6/saving/concurrency how to deal with 
        /// concurrency while saving. Add only check that if concurrecy exception - entity should exists in database.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
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
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw ex;
            }
            

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}
