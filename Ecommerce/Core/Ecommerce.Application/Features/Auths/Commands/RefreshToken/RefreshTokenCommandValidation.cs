using FluentValidation;

namespace Ecommerce.Application.Features.Auths.Commands.RefreshToken;

public class RefreshTokenCommandValidation : AbstractValidator<RefreshTokenCommandRequest>
{
    public RefreshTokenCommandValidation()
    {
        RuleFor(x=>x.AccessToken).NotEmpty();
        RuleFor(x=>x.RefreshToken).NotEmpty();
    }
}
