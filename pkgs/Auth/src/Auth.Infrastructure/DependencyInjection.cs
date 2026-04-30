using Auth.Domain.Services;
using Auth.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IEncrypter, Encrypter>();
    }
}
