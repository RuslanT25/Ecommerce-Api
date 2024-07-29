using MediatR;

namespace Ecommerce.Application.Features.Auths.Commands.RefreshToken;

public class RefreshTokenCommandRequest : IRequest<RefreshTokenCommandResponse>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
