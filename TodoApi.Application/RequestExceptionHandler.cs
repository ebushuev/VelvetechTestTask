using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using NLog;

namespace TodoApi.Application
{
    public class RequestExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TRequest : IRequest<TResponse>
        where TException : Exception
    {

        private readonly ILogger _logger;

        public RequestExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task Handle(TRequest request,
            TException exception,
            RequestExceptionHandlerState<TResponse> state,
            CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            _logger.Error(exception, $"{name}: {exception.Message}");

            state.SetHandled(default);

            return Task.CompletedTask;
        }
    }
}
