using FluentValidation;

namespace Ecommerce.Application.Features.Auths.Commands.Revoke;

public class RevokeCommandValidation : AbstractValidator<RevokeCommandRequest>
{
    public RevokeCommandValidation()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
