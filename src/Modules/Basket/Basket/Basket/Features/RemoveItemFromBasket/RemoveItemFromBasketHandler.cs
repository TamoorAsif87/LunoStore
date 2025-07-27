namespace Basket.Basket.Features.RemoveItemFromBasket;

public record RemoveItemCommand(string UserName, Guid ProductId) : ICommand;

public class RemoveItemCommandValidator : AbstractValidator<RemoveItemCommand>
{
    public RemoveItemCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
    }
}

public class RemoveItemCommandHandler(IBasketRepository repository)
    : ICommandHandler<RemoveItemCommand>
{
    public async Task<Unit> Handle(RemoveItemCommand command, CancellationToken cancellationToken)
    {
        var cart = await repository.GetBasket(command.UserName,false, cancellationToken);
        cart.RemoveItem(command.ProductId);
        await repository.SaveChanges(command.UserName, cancellationToken);
        return Unit.Value;
    }
}
