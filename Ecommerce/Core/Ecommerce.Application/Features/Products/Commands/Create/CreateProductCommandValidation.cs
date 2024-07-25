using FluentValidation;

namespace Ecommerce.Application.Features.Products.Commands.Create; 

public class CreateProductCommandValidation : AbstractValidator<CreateProductCommandRequest>
{
    public CreateProductCommandValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x=>x.Price)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x=>x.BrandId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x=>x.Discount)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.CategoryIds)
            .NotEmpty()
            .Must(categories => categories.Any());
    }
}
