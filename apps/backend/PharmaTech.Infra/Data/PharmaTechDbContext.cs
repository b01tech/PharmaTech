using Microsoft.EntityFrameworkCore;
using PharmaTech.Product.Category.Models;

namespace PharmaTech.Infra.Data;

public class PharmaTechDbContext(DbContextOptions<PharmaTechDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Subcategory> Subcategories { get; set; } = null!;
    public DbSet<Product.Product.Models.Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PharmaTechDbContext).Assembly);
    }
}
