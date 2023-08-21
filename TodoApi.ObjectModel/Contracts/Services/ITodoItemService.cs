namespace TodoApi.ObjectModel.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TodoApi.ObjectModel.Models;

    public interface ITodoItemService
    {
        Task CreateAsync(TodoItem item, CancellationToken cancellationToken = default);

        Task UpdateAsync(long id, TodoItem updatedItem, CancellationToken cancellationToken);

        Task<TodoItem> GetAsync(long id, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<TodoItem>> GetItemsAsync(CancellationToken cancellationToken = default);

        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}