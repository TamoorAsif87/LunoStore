namespace Basket.Basket.Models;

public class ShoppingCartItem:Entity<Guid>
{
    public Guid ProductId { get; private set; } = default!;
    public Guid ShoppingCartId { get; private set; }=default!;
    public int Quantity { get; internal set; }=default;
    public decimal Price { get; private set; } = default!;
    public string ProductName { get; private set; } = default!;
    public List<string> ProductColors { get; private set; } = default!;
    public List<string> ProductImages { get; private set; } = default!;


   

    internal ShoppingCartItem(Guid productId, Guid shoppingCartId, int quantity, decimal price, string productName, List<string> productColors, List<string> productImages)
    {
       
        ProductId = productId;
        ShoppingCartId = shoppingCartId;
        Quantity = quantity;
        Price = price;
        ProductName = productName;
        ProductColors = productColors;
        ProductImages = productImages;
    }



    [JsonConstructor]
    public ShoppingCartItem(Guid id,Guid productId, Guid shoppingCartId, int quantity, decimal price, string productName, List<string> productColors,List<string> productImages)
    {
        Id = id;
        ProductId = productId;
        ShoppingCartId = shoppingCartId;
        Quantity = quantity;
        Price = price;
        ProductName = productName;
        ProductColors = productColors;
        ProductImages = productImages;
    }
    public decimal ItemCost => Quantity * Price;

    public void Update(decimal price)
    {
        Price = price;
    }
}
