using System.Threading.Tasks;
using TodoApiDTO.DAL.Contexts;
using TodoApiDTO.DAL.Repositories.Abstractions;
using TodoApiDTO.DAL.Repositories.Implementations;

namespace TodoApiDTO.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoDbContext _dbContext;

        public UnitOfWork(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ITodoItemRepository _todoItemRepository;
        public ITodoItemRepository TodoItemRepository => _todoItemRepository ?? new TodoItemRepository(_dbContext);

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        public void Dispose() => _dbContext.Dispose();
    }
}
