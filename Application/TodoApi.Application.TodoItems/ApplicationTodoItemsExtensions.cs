using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApi.Application.TodoItems
{
    public static class ApplicationTodoItemsExtensions
    {
        public static IServiceCollection AddTodoItemHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(ApplicationTodoItemsExtensions));
            serviceCollection.AddValidatorsFromAssembly(typeof(ApplicationTodoItemsExtensions).Assembly);

            return serviceCollection;
        }
    }
}