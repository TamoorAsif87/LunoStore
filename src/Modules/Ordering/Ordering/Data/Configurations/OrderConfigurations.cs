
using Ordering.Orders.Models;

namespace Ordering.Data.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

       

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(o => o.OrderId);

        builder.ComplexProperty(
            o => o.ShippingAddress, builderShippingAddress =>
            {
                builderShippingAddress.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                builderShippingAddress.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                builderShippingAddress.Property(a => a.EmailAddress).HasMaxLength(50).IsRequired();
                builderShippingAddress.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                builderShippingAddress.Property(a => a.City).HasMaxLength(50).IsRequired();
                builderShippingAddress.Property(a => a.State).HasMaxLength(50).IsRequired();
                builderShippingAddress.Property(a => a.Country).HasMaxLength(50).IsRequired();
                builderShippingAddress.Property(a => a.ZipCode).HasMaxLength(5).IsRequired();

            });


        builder.ComplexProperty(
           o => o.BillingsAddress, builderAddress =>
           {
               builderAddress.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
               builderAddress.Property(a => a.LastName).HasMaxLength(50).IsRequired();
               builderAddress.Property(a => a.EmailAddress).HasMaxLength(50).IsRequired();
               builderAddress.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
               builderAddress.Property(a => a.City).HasMaxLength(50).IsRequired();
               builderAddress.Property(a => a.State).HasMaxLength(50).IsRequired();
               builderAddress.Property(a => a.Country).HasMaxLength(50).IsRequired();
               builderAddress.Property(a => a.ZipCode).HasMaxLength(5).IsRequired();

           });


        builder.ComplexProperty(
            o => o.Payment, builderPayment =>
            {
                builderPayment.Property(p => p.CardName)
                .HasMaxLength(50);
                
                builderPayment.Property(p => p.CardNumber)
                              .HasMaxLength(24)
                              .IsRequired();
                
                builderPayment.Property(p => p.Expiration)
                              .IsRequired();
                
                builderPayment.Property(p => p.CVV)
                              .HasMaxLength(3)
                              .IsRequired();
            });
    }
}
