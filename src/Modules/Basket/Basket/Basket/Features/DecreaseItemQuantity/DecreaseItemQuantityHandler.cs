namespace Basket.Basket.Features.DecreaseItemQuantity;

public record DecreaseItemQuantityCommand(string UserName, Guid ProductId, int Quantity) : ICommand;


public class DecreaseItemQuantityCommandValidator : AbstractValidator<DecreaseItemQuantityCommand>
{
    public DecreaseItemQuantityCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}

public class DecreaseItemQuantityCommandHandler(IBasketRepository repository)
    : ICommandHandler<DecreaseItemQuantityCommand>
{
    public async Task<Unit> Handle(DecreaseItemQuantityCommand command, CancellationToken cancellationToken)
    {
        var cart = await repository.GetBasket(command.UserName,false, cancellationToken);
        cart.DecreaseItemQuantity(command.ProductId, command.Quantity);
        await repository.SaveChanges(command.UserName, cancellationToken);
        return Unit.Value;
    }
}

