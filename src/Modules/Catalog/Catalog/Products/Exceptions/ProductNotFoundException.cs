namespace Catalog.Products.Exceptions;

public class ProductNotFoundException:NotFoundException
{
    public ProductNotFoundException(string name, object key):base(name,key)
    {
        
    }
}
