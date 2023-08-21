namespace Microsoft.Extensions.DependencyInjection
{
    using TodoApi.Logic;
    using TodoApi.ObjectModel.Contracts.Services;

    public static class TodoLogicServiceCollectionExtensions
    {
        public static IServiceCollection AddTodoLogic(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddScoped<ITodoItemService, TodoItemService>();
    }
}