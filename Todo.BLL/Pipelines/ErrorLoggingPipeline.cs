using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Todo.Common.Exceptions;

namespace Todo.BLL.Pipelines
{
    public class ErrorLoggingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<IPipelineBehavior<TRequest, TResponse>> _logger;

        public ErrorLoggingPipeline(ILogger<IPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            try
            {
                return await next.Invoke();
            }
            catch(NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException, "An attempt to use non-existent entity.");
                throw;
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "An unexpected error occured while executing request.");
                throw;
            }
        }
    }
}
