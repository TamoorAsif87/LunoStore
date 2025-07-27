namespace Ordering.Orders.Features.UpdateOrder;


public record UpdateOrderCommand(OrderDto Order) : ICommand;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull();

        When(x => x.Order is not null, () =>
        {
            RuleForEach(x => x.Order.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId).NotEmpty();
                items.RuleFor(i => i.Quantity).GreaterThan(0);
                items.RuleFor(i => i.Price).GreaterThan(0);
                items.RuleFor(i => i.ProductName).NotEmpty();
            });

            RuleFor(x => x.Order.Payment.CardNumber).NotEmpty();
            RuleFor(x => x.Order.Payment.CVV).NotEmpty();
            RuleFor(x => x.Order.Payment.Expiration).NotEmpty();

            RuleFor(x => x.Order.ShippingAddress.FirstName).NotEmpty();
            RuleFor(x => x.Order.ShippingAddress.LastName).NotEmpty();
            RuleFor(x => x.Order.ShippingAddress.EmailAddress).EmailAddress();
            RuleFor(x => x.Order.ShippingAddress.AddressLine).NotEmpty();
            RuleFor(x => x.Order.ShippingAddress.ZipCode).NotEmpty();
            RuleFor(x => x.Order.ShippingAddress.Country).NotEmpty();
            RuleFor(x => x.Order.ShippingAddress.City).NotEmpty();
            RuleFor(x => x.Order.ShippingAddress.State).NotEmpty();
        });
    }
}

internal class UpdateOrderCommandHandler(OrderDbContext dbContext, IMapper mapper)
    : ICommandHandler<UpdateOrderCommand>
{
    public async Task<Unit> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderDto = command.Order;

        var existingOrder = await dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderDto.OrderId, cancellationToken);

        if (existingOrder is null)
            throw new OrderNotFoundException(orderDto.OrderId);

        existingOrder.UpdateShippingAddress(mapper.Map<Address>(orderDto.ShippingAddress));
        existingOrder.UpdateBillingAddress(mapper.Map<Address>(orderDto.BillingsAddress));
        existingOrder.UpdatePayment(mapper.Map<Payment>(orderDto.Payment));

        // Map and replace order items using your domain method
        var newItems = orderDto.Items
            .Select(i => mapper.Map<OrderItem>(i))
            .ToList();

        existingOrder.ReplaceOrderItems(newItems);

        await dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}