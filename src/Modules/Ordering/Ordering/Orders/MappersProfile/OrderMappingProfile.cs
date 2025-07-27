namespace Ordering.Orders.MappersProfile;
public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        // Domain => DTOs (records need constructor mapping!)
        CreateMap<Address, AddressDto>()
            .ConstructUsing(src => new AddressDto(
                src.FirstName, src.LastName, src.EmailAddress, src.AddressLine,
                src.Country, src.ZipCode, src.City, src.State));

        CreateMap<Payment, PaymentDto>()
            .ConstructUsing(src => new PaymentDto(
                src.CardName, src.CardNumber, src.Expiration, src.CVV));

        CreateMap<OrderItem, OrderItemDto>()
            .ConstructUsing(src => new OrderItemDto(
                src.ProductId, src.Price, src.Quantity, src.OrderId,
                src.ProductImages, src.ProductName, src.ProductColors));

        CreateMap<Order, OrderDto>()
            .ConstructUsing(src => new OrderDto(
                src.Id,
                src.Items.Select(i => new OrderItemDto(
                    i.ProductId, i.Price, i.Quantity, i.OrderId,
                    i.ProductImages, i.ProductName, i.ProductColors)).ToList(),
                src.CustomerId,
                new AddressDto(
                    src.ShippingAddress.FirstName, src.ShippingAddress.LastName,
                    src.ShippingAddress.EmailAddress, src.ShippingAddress.AddressLine,
                    src.ShippingAddress.Country, src.ShippingAddress.ZipCode,
                    src.ShippingAddress.City, src.ShippingAddress.State),
                new AddressDto(
                    src.BillingsAddress.FirstName, src.BillingsAddress.LastName,
                    src.BillingsAddress.EmailAddress, src.BillingsAddress.AddressLine,
                    src.BillingsAddress.Country, src.BillingsAddress.ZipCode,
                    src.BillingsAddress.City, src.BillingsAddress.State),
                new PaymentDto(
                    src.Payment.CardName, src.Payment.CardNumber,
                    src.Payment.Expiration, src.Payment.CVV)));

        // DTOs => Domain (Value Objects)
        CreateMap<AddressDto, Address>()
            .ConvertUsing(src => Address.Of(
                src.FirstName, src.LastName, src.EmailAddress, src.AddressLine,
                src.Country, src.City, src.State, src.ZipCode));

        CreateMap<PaymentDto, Payment>()
            .ConvertUsing(src => Payment.Of(
                src.CardName!, src.CardNumber, src.Expiration, src.CVV));

        CreateMap<OrderItemDto, OrderItem>()
            .ConstructUsing(src => new OrderItem(
                src.ProductId, src.OrderId, src.Price, src.Quantity,
                src.ProductImages, src.ProductColors, src.ProductName));

        CreateMap<OrderDto, Order>()
            .ConstructUsing((src, context) =>
            {
                var shipping = context.Mapper.Map<Address>(src.ShippingAddress);
                var billing = context.Mapper.Map<Address>(src.BillingsAddress);
                var payment = context.Mapper.Map<Payment>(src.Payment);
                return Order.Create(src.OrderId, src.CustomerId!, shipping, billing, payment);
            })
            .ForMember(dest => dest.Items, opt => opt.Ignore())
            .AfterMap((src, dest, context) =>
            {
                var items = src.Items.Select(i => context.Mapper.Map<OrderItem>(i));
                dest.ReplaceOrderItems(items);
            });
    }
}
