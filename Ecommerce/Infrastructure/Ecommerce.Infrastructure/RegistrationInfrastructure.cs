using Ecommerce.Infrastructure.Services.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure;

public static class RegistrationInfrastructure
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<TokenOption>(configuration.GetSection("TokenOption"));
    }
}
