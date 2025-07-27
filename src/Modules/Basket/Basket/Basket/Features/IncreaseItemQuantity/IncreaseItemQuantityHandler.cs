
namespace Basket.Basket.Features.IncreaseItemQuantity;

public record IncreaseItemQuantityCommand(string UserName,Guid ProductId,int Quantity):ICommand;



public class IncreaseItemQuantityCommandValidator : AbstractValidator<IncreaseItemQuantityCommand>
{
    public IncreaseItemQuantityCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}

internal class IncreaseItemQuantityHandler(IBasketRepository basketRepository) : ICommandHandler<IncreaseItemQuantityCommand>
{
    public async Task<Unit> Handle(IncreaseItemQuantityCommand command, CancellationToken cancellationToken)
    {
        var cart = await basketRepository.GetBasket(command.UserName, false, cancellationToken);
        cart.IncreaseItemQuantity(command.ProductId, command.Quantity);
        await basketRepository.SaveChanges(command.UserName,cancellationToken);
        return Unit.Value;
    }
}
