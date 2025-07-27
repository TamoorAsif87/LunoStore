namespace Catalog.Data.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProductName)
           .IsRequired()
           .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.InStock)
            .IsRequired();

        builder.Property(p => p.ProductImages).HasColumnType("text[]");
        builder.Property(p => p.ProductColors).HasColumnType("text[]");



        builder.HasOne(p => p.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.CategoryId);
    }
}
