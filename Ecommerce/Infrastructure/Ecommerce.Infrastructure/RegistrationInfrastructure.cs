using Ecommerce.Infrastructure.Services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce.Infrastructure;

public static class RegistrationInfrastructure
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<TokenOption>(configuration.GetSection(nameof(TokenOption)));
        var tokenOption = configuration.GetSection(nameof(TokenOption)).Get<TokenOption>();

        services.AddAuthentication(opt =>   
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
        {
            opt.SaveToken = true;
            opt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption!.SecretKey!)),
                ValidateLifetime = false,
                ValidIssuer = tokenOption.Issuer,
                ValidAudience = tokenOption.Audience,
                ClockSkew = TimeSpan.Zero
            };
        });


    }
}
