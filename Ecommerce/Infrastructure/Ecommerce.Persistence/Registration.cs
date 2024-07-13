using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Persistence.Context;
using Ecommerce.Persistence.Repositories;
using Ecommerce.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Persistence
{
    public static class Registration
    {
        public static void AddPersistance(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<EcommerceDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("SqlCon")));

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
