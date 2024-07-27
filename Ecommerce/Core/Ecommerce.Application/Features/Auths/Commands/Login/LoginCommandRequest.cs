using MediatR;
using System.ComponentModel;

namespace Ecommerce.Application.Features.Auths.Commands.Login;

public class LoginCommandRequest : IRequest<LoginCommandResponse>
{
    [DefaultValue("ruslan@gmail.com")]
    public string Email { get; set; }

    [DefaultValue("string")]
    public string Password { get; set; }
}
