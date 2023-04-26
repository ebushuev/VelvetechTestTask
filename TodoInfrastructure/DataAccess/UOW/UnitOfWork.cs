using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TodoCore.Data.Interfaces;

namespace TodoInfrastructure.DataAccess.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ITodoItemReposytory _todoItemReposytory;

        public ITodoItemReposytory TodoItemReposytory => _todoItemReposytory;
        public UnitOfWork(ApplicationDbContext context, ITodoItemReposytory todoItemReposytory)
        {
            _context = context;
            _todoItemReposytory = todoItemReposytory;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public IDbTransaction StartTransation()
        {
            var transaction = _context.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }
    }
}
