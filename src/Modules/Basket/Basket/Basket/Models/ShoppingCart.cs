namespace Basket.Basket.Models;

public class ShoppingCart:Aggregate<Guid>
{
    public string UserName { get; private set; } = default!;
    private List<ShoppingCartItem> _items = new();
    public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();
    public decimal TotalPrice => _items.Sum(item => item.ItemCost);

    public static ShoppingCart Create(Guid id, string userName)
    {
        var shoppingCart = new ShoppingCart
        {
            Id = id,
            UserName = userName,
        };
        return shoppingCart;
    }

    public void AddItem(Guid productId, int quantity, decimal price, string productName, List<string> productColors,List<string> productImages)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        var itemExist = _items.FirstOrDefault(item => item.ProductId == productId);
        if (itemExist == null)
        {
            var newItem = new ShoppingCartItem(productId, Id, quantity, price, productName, productColors,productImages);
            _items.Add(newItem);
        }
        else
        {
            itemExist.Quantity += quantity;
        }

    }

    public void RemoveItem(Guid productId)
    {
        var itemExist = _items.FirstOrDefault(item => item.ProductId == productId);
        if (itemExist != null)
        {
            _items.Remove(itemExist);
        }
    }

    public void IncreaseItemQuantity(Guid productId, int quantity)
    {
        var itemExist = _items.FirstOrDefault(item => item.ProductId == productId);
        if (itemExist != null)
        {
            itemExist.Quantity += quantity;
        }
    }

    public void DecreaseItemQuantity(Guid productId, int quantity)
    {
        var itemExist = _items.FirstOrDefault(item => item.ProductId == productId);
        if (itemExist != null)
        {
            if(itemExist.Quantity == 1 || (itemExist.Quantity - Math.Abs(quantity) <= 0))
            {
                _items.Remove(itemExist);
            }
            else
            {
                itemExist.Quantity -= quantity;
            }
        }
    }

    public string GetColor(List<string> productColors, string productColor)
    {
        if (productColors != null && productColors.Count > 0)
        {
            var color = productColors.FirstOrDefault(c => c.ToLowerInvariant() == productColor.ToLowerInvariant());
            if (color != null)
            {
                return color;
            }
            else
            {
                return productColors[0];
            }
        }
        else
        {
            return productColor;
        }
    }
}
