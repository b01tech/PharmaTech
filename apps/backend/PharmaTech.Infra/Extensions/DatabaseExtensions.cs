using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PharmaTech.Infra.Data;

namespace PharmaTech.Infra.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PharmaTechDbContext>();
        
        await context.Database.MigrateAsync();
        await DbSeeder.SeedAsync(context);
    }
}
