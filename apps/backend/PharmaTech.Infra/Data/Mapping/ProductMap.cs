using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaTech.Product.Category.Models;
using PharmaTech.Product.Product.Models;

namespace PharmaTech.Infra.Data.Mapping;

public class ProductMap : IEntityTypeConfiguration<Product.Product.Models.Product>
{
    public void Configure(EntityTypeBuilder<Product.Product.Models.Product> builder)
    {
        builder.ToTable("Products");

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

        builder.OwnsOne(x => x.Description, d =>
        {
            d.Property(x => x.Value)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsRequired();
        });

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.OwnsOne(x => x.Sku, s =>
        {
            s.Property(x => x.Value)
                .HasColumnName("Sku")
                .HasMaxLength(50)
                .IsRequired();

            s.HasIndex(x => x.Value).IsUnique();
        });

        builder.Property(x => x.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasOne<Subcategory>()
            .WithMany()
            .HasForeignKey(x => x.SubcategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
