
using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketCommand(ShoppingCartDto ShoppingCart) : ICommand<Guid>;

public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart.UserName).NotEmpty();
    }
}


internal class CreateBasketCommandHandler(IBasketRepository repository,IMediator mediator)
    : ICommandHandler<CreateBasketCommand,Guid>
{
    public async Task<Guid> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = await CreateShoppingCart(command.ShoppingCart);
        await repository.CreateBasket(cart, cancellationToken);
        return cart.Id;
    }

    private async Task<ShoppingCart> CreateShoppingCart(ShoppingCartDto shoppingCart)
    {
        var cart = ShoppingCart.Create(Guid.NewGuid(), shoppingCart.UserName);

        foreach (var item in shoppingCart.Items)
        {
            var product = await mediator.Send(new GetProductByIdQuery(item.ProductId));
            cart.AddItem(product.id, item.Quantity,product.price, product.productName,item.ProductColors,item.ProductImages);
        }
        return cart;
    }

    
}
