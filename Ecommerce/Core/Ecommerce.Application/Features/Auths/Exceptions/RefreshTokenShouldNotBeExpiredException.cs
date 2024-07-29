using Ecommerce.Application.Bases;

namespace Ecommerce.Application.Features.Auths.Exceptions;

public class RefreshTokenShouldNotBeExpiredException : BaseException
{
    public RefreshTokenShouldNotBeExpiredException() : base("You've reached your expiry date.Login again.") { }
}
