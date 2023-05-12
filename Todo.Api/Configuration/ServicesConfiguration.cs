using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Todo.BLL.Pipelines;

namespace Todo.Api.Configuration
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(ErrorLoggingPipeline<,>).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ErrorLoggingPipeline<,>));
            });
            services.AddControllers();
        }
    }
}
