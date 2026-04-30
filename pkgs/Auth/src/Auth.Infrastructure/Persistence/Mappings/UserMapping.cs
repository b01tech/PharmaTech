using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaTech.Shared.ValueObjects;

namespace Auth.Infrastructure.Persistence.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.OwnsOne(
            u => u.Name,
            name =>
            {
                name.Property(n => n.Value).HasColumnName("UserName").HasMaxLength(Name.DefaultMaxLenght).IsRequired();
                name.HasIndex(n => n.Value).IsUnique();
            }
        );
        builder.OwnsOne(
            u => u.Email,
            email =>
            {
                email.Property(e => e.Address).HasColumnName("Email").HasMaxLength(255).IsRequired();
                email.HasIndex(e => e.Address).IsUnique();
            }
        );
        builder.OwnsOne(u => u.Role, role => role.Property(r => r.Name).HasColumnName("Role").IsRequired());
        builder.Property(u => u.AvatarUrl).HasColumnName("AvatarUrl").HasMaxLength(255);
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.UpdatedAt);
        builder.Property(u => u.DeletedAt);
    }
}
