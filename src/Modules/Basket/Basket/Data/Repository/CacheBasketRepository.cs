
using Basket.Basket.JsonConverters;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Data.Repository;

public class CacheBasketRepository(IBasketRepository _basketRepository,IDistributedCache _cache) : IBasketRepository
{
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = {new ShoppingCartJsonConverter(), new ShoppingCartItemJsonConverter()}
    };

    public async Task<ShoppingCart> CreateBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        var cart =  await _basketRepository.CreateBasket(shoppingCart, cancellationToken);
        await _cache.SetStringAsync(shoppingCart.UserName,JsonSerializer.Serialize(shoppingCart,_options));
        return cart;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        var result =  await _basketRepository.DeleteBasket(userName, cancellationToken);
        await _cache.RemoveAsync(userName, cancellationToken);
        return result;
    }

    public async Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        if (!asNoTracking)
        {
            return await _basketRepository.GetBasket(userName, false, cancellationToken);
        }

        var cacheResult = await _cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cacheResult))
            return JsonSerializer.Deserialize<ShoppingCart>(cacheResult, _options)!;

        var basket = await _basketRepository.GetBasket(userName,asNoTracking, cancellationToken);
        await _cache.SetStringAsync(userName,JsonSerializer.Serialize(basket, _options));
        return basket;
    }

    public async Task<int> SaveChanges(string? username, CancellationToken cancellationToken = default)
    {
        if(username != null)
        {
            await _cache.RemoveAsync($"{username}");
        }

        return await _basketRepository.SaveChanges(username, cancellationToken); 
    }
}
