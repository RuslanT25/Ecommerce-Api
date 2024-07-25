using FluentValidation;
using MediatR;

namespace Ecommerce.Application.Behaviours;

public class FluentValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validator;

    public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
    {
        _validator = validator;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var fails = _validator
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .GroupBy(x => x.ErrorMessage)
            .Select(x => x.First())
            .Where(x => x != null)
            .ToList();

        if (fails.Count != 0)
            throw new ValidationException(fails);

        return next();
    }
}
