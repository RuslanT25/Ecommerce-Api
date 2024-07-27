using FluentValidation;

namespace Ecommerce.Application.Features.Auths.Commands.Register;

public class RegisterCommandValidation : AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandValidation()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(2);

        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(60)
            .EmailAddress()
            .MinimumLength(8);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .MinimumLength(6)
            .Equal(x => x.Password);
    }
}
