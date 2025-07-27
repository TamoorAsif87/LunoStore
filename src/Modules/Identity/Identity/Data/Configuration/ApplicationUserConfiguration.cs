using Identity.Account.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Data.Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users", "identity");

        builder.Property(u => u.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Address)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.Phone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(u => u.Country)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.City)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.ProfileImage)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasIndex(u => u.Email);
        builder.HasIndex(u => u.UserName);

    }
}
