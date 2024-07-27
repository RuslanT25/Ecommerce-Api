using MediatR;

namespace Ecommerce.Application.Features.Auths.Commands.Register;

public class RegisterCommandRequest : IRequest<Unit>
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
