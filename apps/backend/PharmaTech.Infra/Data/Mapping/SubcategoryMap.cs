using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaTech.Product.Category.Models;

namespace PharmaTech.Infra.Data.Mapping;

public class SubcategoryMap : IEntityTypeConfiguration<Subcategory>
{
    public void Configure(EntityTypeBuilder<Subcategory> builder)
    {
        builder.ToTable("Subcategories");

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
    }
}
