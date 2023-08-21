namespace TodoApi.Logic
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TodoApi.ObjectModel.Contracts.Repositories;
    using TodoApi.ObjectModel.Contracts.Services;
    using TodoApi.ObjectModel.Models;
    using TodoApi.ObjectModel.Models.Exceptions;

    internal sealed class TodoItemService : ITodoItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITodoItemsRepository _repository;

        public TodoItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.Repository<ITodoItemsRepository>();
        }

        public async Task CreateAsync(TodoItem item, CancellationToken cancellationToken)
        {
            _repository.Add(item);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateAsync(long id, TodoItem updatedItem, CancellationToken cancellationToken)
        {
            await _unitOfWork.ExecuteUnderTransactionAsync(Update, cancellationToken);

            async Task Update()
            {
                var existingItem = await _repository.FindAsync(id, cancellationToken);
                if (existingItem == null)
                {
                    throw new NotFoundException($"Item with id '{id}' was not found in repository");
                }

                existingItem.Name = updatedItem.Name;
                existingItem.IsComplete = updatedItem.IsComplete;
            }
        }

        public async Task<TodoItem> GetAsync(long id, CancellationToken cancellationToken)
        {
            var item = await _repository.FindAsync(id, cancellationToken);

            if (item == null)
            {
                throw new NotFoundException($"Item with id '{id}' was not found");
            }

            return item;
        }

        public Task<IReadOnlyCollection<TodoItem>> GetItemsAsync(CancellationToken cancellationToken)
            => _repository.GetItemsAsync(cancellationToken);

        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            await _unitOfWork.ExecuteUnderTransactionAsync(Delete, cancellationToken);

            async Task Delete()
            {
                var item = await _repository.FindAsync(id, cancellationToken);

                if (item == null)
                {
                    throw new NotFoundException($"Item with id '{id}' was not found in repository");
                }

                _repository.Delete(item);
            }
        }
    }
}