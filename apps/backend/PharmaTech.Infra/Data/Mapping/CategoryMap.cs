using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaTech.Product.Category.Models;

namespace PharmaTech.Infra.Data.Mapping;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Name, n =>
        {
            n.Property(x => x.Value)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Alias, a =>
        {
            a.Property(x => x.Value)
                .HasColumnName("Alias")
                .HasMaxLength(100)
                .IsRequired();

            a.HasIndex(x => x.Value).IsUnique();
        });

        builder.HasMany(x => x.Subcategories)
            .WithOne()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
