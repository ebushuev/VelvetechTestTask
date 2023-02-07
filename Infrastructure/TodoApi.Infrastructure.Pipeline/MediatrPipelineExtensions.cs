using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApi.Infrastructure.Pipeline
{
    public static class MediatrPipelineExtensions
    {
        public static IServiceCollection AddMediatrRequestValidation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatrValidationPipelineBehavior<,>));
            
            return serviceCollection;
        }
    }
}