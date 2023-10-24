using FluentValidation;
using FluentValidation.TestHelper;
using MediatR;

namespace Infeco.Api.Infrastructure.MediatR
{
    // https://stackoverflow.com/questions/42283011/add-validation-to-a-mediatr-behavior-pipeline
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest :  IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);

            return next();
        }
    }
}
