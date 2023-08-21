namespace TodoApi.Repository
{
    using TodoApi.ObjectModel.Contracts.Repositories;

    internal sealed class TodoUnitOfWork : UnitOfWork
    {
        public TodoUnitOfWork(
            TodoContext context,
            ITodoItemsRepository todoItemsRepository) 
            : base(context)
        {
            RegisterRepository(todoItemsRepository, typeof(ITodoItemsRepository));
        }
    }
}