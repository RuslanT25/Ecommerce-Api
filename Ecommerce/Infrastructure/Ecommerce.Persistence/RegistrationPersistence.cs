using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;
using Ecommerce.Persistence.Context;
using Ecommerce.Persistence.Repositories;
using Ecommerce.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ecommerce.Persistence;

public static class RegistrationPersistence
{
    public static void AddPersistance(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<EcommerceDbContext>(opt =>
        opt.UseSqlServer(configuration.GetConnectionString("SqlCon")));

        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddIdentityCore<AppUser>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 2;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireDigit = false;
            opt.SignIn.RequireConfirmedEmail = false;
        })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<EcommerceDbContext>();
    }
}
