namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using TodoApi.ObjectModel.Contracts.Repositories;
    using TodoApi.ObjectModel.Models.Settings;
    using TodoApi.Repository;
    using TodoApi.Repository.Repositories;

    public static class TodoRepositoriesServiceCollectionExtensions
    {
        public static IServiceCollection AddTodoRepositories(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();

            serviceCollection.AddDbContext<TodoContext>(opt =>
                opt.UseSqlServer(connectionStrings.MsSqlDatabase))
                .AddScoped<IUnitOfWork, TodoUnitOfWork>();

            return serviceCollection
                .AddScoped<ITodoItemsRepository, TodoItemsRepository>();
        }
    }
}