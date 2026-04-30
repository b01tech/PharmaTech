using Auth.Domain.Repositories;
using Auth.Domain.Services;
using Auth.Infrastructure.Persistence.Repositories;
using Auth.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        AddRepositories(services);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IEncrypter, Encrypter>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteRepository, UserRepository>();
        services.AddScoped<IPasswordRepository, PasswordRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    }
}
