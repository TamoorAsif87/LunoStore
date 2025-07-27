namespace Basket.Data.Configurations;

public class ShoppingCartItemConfigurations : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.ToTable("ShoppingCartItems");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.ProductId)
               .IsRequired();

        builder.Property(item => item.ShoppingCartId)
               .IsRequired();

        builder.Property(item => item.Quantity)
               .IsRequired();

        builder.Property(item => item.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(item => item.ProductName)
               .IsRequired()
               .HasMaxLength(100);

       
    }
}
