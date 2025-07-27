using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Basket.Features.AddItemToBasket;

public record AddItemCommand(string UserName, ShoppingCartItemDto Item) : ICommand;

public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    public AddItemCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Item.ProductId).NotEmpty();
        RuleFor(x => x.Item.Quantity).GreaterThan(0);
        RuleFor(x => x.Item.Price).GreaterThan(0);
        RuleFor(x => x.Item.ProductName).NotEmpty();
        RuleFor(x => x.Item.ProductColors).NotEmpty();
    }
}


internal class AddItemCommandHandler(IBasketRepository repository,IMediator sender)
    : ICommandHandler<AddItemCommand>
{
    public async Task<Unit> Handle(AddItemCommand command, CancellationToken cancellationToken)
    {
        var cart = await repository.GetBasket(command.UserName,asNoTracking:false, cancellationToken);

        var product = await sender.Send(new GetProductByIdQuery(command.Item.ProductId));

        cart.AddItem(
            product.id,
            command.Item.Quantity,
            product.price,
            product.productName,
           product.productColors, product.productImages
        );

        await repository.SaveChanges(command.UserName, cancellationToken);
        return Unit.Value;
    }

   
}
