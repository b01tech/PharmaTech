using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmaTech.Infra.Data;
using PharmaTech.Infra.Repositories;
using PharmaTech.Product.Category.Repositories;
using PharmaTech.Product.Product.Repositories;

namespace PharmaTech.Infra.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddContext(services, configuration);
        AddRepositories(services);
        return services;
    }

    private static void AddContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        services.AddDbContext<PharmaTechDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<ICategoryWriteRepository, CategoryRepository>();
        services.AddScoped<ICategoryReadOnlyRepository, CategoryRepository>();
        services.AddScoped<IProductWriteRepository, ProductRepository>();
        services.AddScoped<IProductReadOnlyRepository, ProductRepository>();
    }
}
