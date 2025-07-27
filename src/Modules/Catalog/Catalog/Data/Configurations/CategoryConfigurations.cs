namespace Catalog.Data.Configurations;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
           .IsRequired()
           .HasMaxLength(100);

    }
}
