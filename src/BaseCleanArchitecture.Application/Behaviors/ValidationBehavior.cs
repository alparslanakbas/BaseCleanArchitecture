using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCleanArchitecture.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
    {

        readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var errorDictionary = _validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .GroupBy(
                    f => f.PropertyName,
                    f => f.ErrorMessage, (propertyName, errorMessage) => new
                    {
                        Key = propertyName,
                        Values = errorMessage.Distinct().ToArray()
                    })
                .ToDictionary(f => f.Key, f => f.Values[0]);

            if (errorDictionary.Any())
            {
                var errors = errorDictionary.Select(e => new ValidationFailure
                {
                    PropertyName = e.Value,
                    ErrorMessage = e.Key
                });

                throw new ValidationException(errors);
            }
            return await next();
        }
    }
}
