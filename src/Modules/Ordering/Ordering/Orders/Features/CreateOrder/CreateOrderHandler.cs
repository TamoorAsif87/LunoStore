
namespace Ordering.Features.CreateOrder;


public record CreateOrderCommand(OrderDto Order) : ICommand<Guid>;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull();

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
    }
}

internal class CreateOrderCommandHandler(OrderDbContext orderDbContext, IMapper mapper) : ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        // Map DTO to Domain Entity
        var order = mapper.Map<Order>(command.Order);

        // Add to DbContext
        await orderDbContext.Orders.AddAsync(order, cancellationToken);

        // Save Changes
        await orderDbContext.SaveChangesAsync(cancellationToken);

        // Return new Order Id
        return order.Id;
    }
}
