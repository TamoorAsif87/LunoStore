namespace Basket.Data.Configurations;

public class ShoppingCartConfigurations : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.ToTable("ShoppingCarts");

        builder.HasKey(cart => cart.Id);

        builder.Property(cart => cart.UserName)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasMany(cart => cart.Items)
            .WithOne()
            .HasForeignKey(cart => cart.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
