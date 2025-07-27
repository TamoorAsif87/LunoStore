using MassTransit.Initializers;

namespace Basket.Basket.Features.UpdateProductItemPrice;

public record UpdateProductItemPriceCommand(Guid ProductId, decimal Price):ICommand<bool>;

public class UpdateProductItemPriceCommandValidator : AbstractValidator<UpdateProductItemPriceCommand>
{
    public UpdateProductItemPriceCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID cannot be empty.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}

internal class UpdateProductItemPriceHandler:ICommandHandler<UpdateProductItemPriceCommand, bool>
{
    private readonly IBasketRepository _basketRepository;
    private readonly BasketContext _basketContext;
    public UpdateProductItemPriceHandler(IBasketRepository basketRepository, BasketContext basketContext)
    {
        _basketRepository = basketRepository;
        _basketContext = basketContext;
    }
    public async Task<bool> Handle(UpdateProductItemPriceCommand command, CancellationToken cancellationToken)
    {
        var itemsToUpdate = await _basketContext.ShoppingCartItems.Where(item => item.ProductId == command.ProductId).ToListAsync();

        if(!itemsToUpdate.Any())
        {
            return false; // No items found with the specified ProductId
        }
       
        foreach (var item in itemsToUpdate)
        {
            item.Update(command.Price);
        }

        await _basketContext.SaveChangesAsync(cancellationToken);

        // after update clear cache of all the baskets which have this productId

        var cartIds = itemsToUpdate.Select(i => i.ShoppingCartId).ToList();

        var carts = await _basketContext.ShoppingCarts.Where(c => cartIds.Contains(c.Id)).ToListAsync();

        var allBasketsUserNames = carts.Select(u => u.UserName).ToList();

        foreach (var userName in allBasketsUserNames)
        {
            await _basketRepository.SaveChanges(userName);
        }

        return true; // Price updated successfully
    }
}

