
namespace Basket.Basket.Features.CheckoutBasket;


public record CheckOutBasketCommand(CheckoutBasketDto CheckoutBasket):ICommand<string>;

public class CheckOutBasketCommandValidator : AbstractValidator<CheckOutBasketCommand>
{
    public CheckOutBasketCommandValidator()
    {
        RuleFor(x => x.CheckoutBasket).NotNull().WithMessage("Basket checkout can not be null");
        RuleFor(x => x.CheckoutBasket.UserName).NotEmpty().WithMessage("Username can not be null");
    }
}

internal class CheckOutBasketCommandHandler(BasketContext basketContext,IMapper mapper,IBasketRepository basketRepository) : ICommandHandler<CheckOutBasketCommand, string>
{
    public async Task<string> Handle(CheckOutBasketCommand command, CancellationToken cancellationToken)
    {
        await using var transaction = await basketContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var basket = await basketContext.ShoppingCarts.
                Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.UserName == command.CheckoutBasket.UserName);

            if(basket == null)
            {
                throw new BasketNotFoundException(command.CheckoutBasket.UserName);
            }
            
            var eventMessages = mapper.Map<BasketCheckoutIntegrationEvent>(command.CheckoutBasket);
            eventMessages.TotalPrice = basket.TotalPrice;
            var OrderId = Guid.NewGuid();
          

            eventMessages.OrderId = OrderId;
            var outboxMessages = new OutboxMessage
            {
                Id  = Guid.NewGuid(),
                Type = typeof(BasketCheckoutIntegrationEvent).AssemblyQualifiedName!,
                Content = JsonSerializer.Serialize(eventMessages),
                OccurredOn = DateTime.UtcNow
            };

            basketContext.OutboxMessages.Add(outboxMessages);

            basketContext.ShoppingCarts.Remove(basket);

            await basketRepository.SaveChanges(command.CheckoutBasket.UserName, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

           

            return OrderId.ToString();
        }
        catch
        {

            await transaction.RollbackAsync(cancellationToken);
            return "";
        }
    }
}
