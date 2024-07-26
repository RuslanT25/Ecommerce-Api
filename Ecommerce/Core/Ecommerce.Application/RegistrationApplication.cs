using Ecommerce.Application.Bases;
using Ecommerce.Application.Behaviours;
using Ecommerce.Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Application;

public static class RegistrationApplication
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddTransient<ExceptionMiddleware>();

        services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));

       // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RedisCacheBehevior<,>));
    }

    // BaseRules-den inheritance alan RuleClass-lari AddTransient-le elave edir.
    private static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services, Assembly assembly, Type type)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            services.AddTransient(item);

        return services;
    }
}
