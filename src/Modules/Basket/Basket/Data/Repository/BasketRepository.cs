using Basket.Basket.Exceptions;

namespace Basket.Data.Repository;

public class BasketRepository(BasketContext context) : IBasketRepository
{
    private readonly BasketContext _context = context;

    public async Task<ShoppingCart> CreateBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        
        _context.ShoppingCarts.Add(shoppingCart);
        await _context.SaveChangesAsync(cancellationToken);
        return shoppingCart;
    }

    public async Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query =  _context.ShoppingCarts
            .Include(c => c.Items)
            .Where(c => c.UserName == userName);

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        var cart = await query.SingleOrDefaultAsync(cancellationToken);

        return cart ?? throw new BasketNotFoundException(userName);
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cart = await GetBasket(userName);

        _context.ShoppingCarts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<int> SaveChanges(string? username, CancellationToken cancellationToken = default)
    {
       
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
