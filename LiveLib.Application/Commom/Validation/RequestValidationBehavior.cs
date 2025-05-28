using FluentValidation;
using LiveLib.Application.Commom.ResultWrapper;
using MediatR;

namespace LiveLib.Application.Commom.Validation
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators) =>
            _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();

            if (failures.Count != 0)
            {
                var type = typeof(TResponse);

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Result<>))
                {
                    var innerType = typeof(TResponse).GetGenericArguments()[0];

                    var failureMethod = typeof(Result<>)
                        .MakeGenericType(innerType)
                        .GetMethod("Validation", [typeof(string)]);

                    if (failureMethod != null)
                    {
                        return (TResponse)failureMethod.Invoke(null, [string.Join(",\n", failures)]);
                    }
                }
                else if (type == typeof(Result))
                {
                    return (TResponse)(object)Result.Validation(string.Join(",\n", failures));
                }

                throw new ValidationException(failures);
            }

            return await next(cancellationToken);
        }
    }
}
