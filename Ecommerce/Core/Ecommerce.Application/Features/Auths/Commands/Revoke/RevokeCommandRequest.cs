using MediatR;

namespace Ecommerce.Application.Features.Auths.Commands.Revoke;

public class RevokeCommandRequest : IRequest<Unit>
{
    public string Email { get; set; }
}
