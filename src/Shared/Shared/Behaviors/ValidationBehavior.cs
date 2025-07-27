using FluentValidation;
using MediatR;
using Shared.Contracts.CQRS;

namespace Shared.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
                        .Where(v => v.Errors.Any())
                        .SelectMany(e => e.Errors)
                        .ToList();

        if(errors.Any())
            throw new ValidationException(errors);

        return await next();
    }
}
