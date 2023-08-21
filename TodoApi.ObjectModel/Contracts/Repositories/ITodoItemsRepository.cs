namespace TodoApi.ObjectModel.Contracts.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TodoApi.ObjectModel.Models;

    public interface ITodoItemsRepository
    {
        void Add(TodoItem item);

        Task<TodoItem> FindAsync(long id, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<TodoItem>> GetItemsAsync(CancellationToken cancellationToken = default);

        void Delete(TodoItem item);
    }
}