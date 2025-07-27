namespace Catalog.Products.Specifications;

public class ProductSpecification:BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecs productSpecs):base(specs => 
        (string.IsNullOrEmpty(productSpecs.SearchProduct) || specs.ProductName.Contains(productSpecs.SearchProduct))
        &&
        (productSpecs.CategoryId == Guid.Empty || specs.CategoryId == productSpecs.CategoryId)
        &&
        (productSpecs.PriceEnd == 0 || (specs.Price >= productSpecs.PriceStart && specs.Price <= productSpecs.PriceEnd))
    )
    {
        switch (productSpecs.SortBy?.ToLower())
        {
            case "price":
                AddOrderBy(p => p.Price);
                break;

            case "-price":
                AddOrderByDescending(p => p.Price);
                break;

            case "name":
                AddOrderBy(p => p.ProductName);
                break;

            case "-name":
                AddOrderByDescending(p => p.ProductName);
                break;

            case "created":
                AddOrderBy(p => p.CreatedAt);  
                break;

            case "-created":
                AddOrderByDescending(p => p.CreatedAt);
                break;

            default:
                AddOrderByDescending(p => p.CreatedAt); 
                break;
        }

        if(productSpecs.isPaginationEnabled)
            ApplyPaging((productSpecs.Page - 1) * productSpecs.PageSize, productSpecs.PageSize);


    }
}
