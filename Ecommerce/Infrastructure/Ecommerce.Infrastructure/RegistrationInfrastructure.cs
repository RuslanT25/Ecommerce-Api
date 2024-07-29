using Ecommerce.Application.Interfaces.RedisCache;
using Ecommerce.Application.Interfaces.Tokens;
using Ecommerce.Infrastructure.Services.RedisCache;
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

        services.AddTransient<ITokenService, TokenService>();

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


        services.Configure<RedisOptions>(configuration.GetSection(nameof(RedisOptions)));
        services.AddTransient<IRedisCacheService, RedisCacheService>();

        var redisOptions = configuration.GetSection(nameof(RedisOptions)).Get<RedisOptions>();

        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = configuration[redisOptions!.ConnectionString];
            opt.InstanceName = configuration[redisOptions!.InstanceName];
        });
    }
}
