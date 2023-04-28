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
        private readonly ITodoItemRepository _todoItemReposytory;

        public ITodoItemRepository TodoItemReposytory => _todoItemReposytory;
        public UnitOfWork(ApplicationDbContext context, ITodoItemRepository todoItemReposytory)
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
