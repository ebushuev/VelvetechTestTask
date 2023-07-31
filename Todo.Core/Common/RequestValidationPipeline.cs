using FluentValidation;
using MediatR;
using Todo.Core.Exceptions;

namespace Todo.Core.Common
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();
            
            var errorsDict = new Dictionary<string, List<string>>();
            
            foreach (var error in failures)
            {
                if (errorsDict.TryGetValue(error.PropertyName, out var row))
                {
                    row.Add(error.ErrorMessage);
                }
                else
                {
                    var messages = new List<string>
                    {
                        error.ErrorMessage
                    };
                    errorsDict.Add(error.PropertyName, messages);
                }
            }

            if (failures.Count != 0)
            {
                throw new BadRequestException("Bad request", errorsDict.ToDictionary(x => x.Key, x => x.Value.AsEnumerable()));
            }

            return next();
        }
    }
}
