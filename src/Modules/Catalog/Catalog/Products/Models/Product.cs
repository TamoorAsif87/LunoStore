namespace Catalog.Products.Models;

public class Product:Aggregate<Guid>
{
    public string ProductName { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int InStock { get; private set; }
    public List<string> ProductImages { get; set; } = default!;
    public List<string> ProductColors { get; set; } = default!;
    public Category? Category { get; set; }
    public Guid CategoryId { get; private set; }

    public static Product Create(Guid id, string productName,string description,decimal price,int inStock, List<string> productImages, List<string> productColors,Guid categoryId)
    {
        ArgumentException.ThrowIfNullOrEmpty(productName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        ArgumentOutOfRangeException.ThrowIfNegative(inStock);

        var product = new Product
        {
            Id = id,
            ProductName = productName,
            Description = description,
            Price = price,
            InStock = inStock,
            ProductImages = productImages,
            ProductColors = productColors,
            CategoryId = categoryId

        };

        product.AddDomainEvent(new ProductCreatedEvent(product));

        return product;
    }

    public void Update(string productName, string description, decimal price, int inStock, List<string> productImages, List<string> productColors, Guid categoryId)
    {
        ArgumentException.ThrowIfNullOrEmpty(productName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        ArgumentOutOfRangeException.ThrowIfNegative(inStock);


        ProductName = productName;
        Description = description;
        InStock = inStock;
        ProductImages = productImages;
        ProductColors = productColors;
        CategoryId = categoryId;

        if (Price != price)
        {
            Price = price;
            AddDomainEvent(new ProductPriceChangedEvent(this));
        }

    }
}
