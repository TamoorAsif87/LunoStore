namespace Catalog.Categories.Exceptions;

public class CategoryNotFoundException:NotFoundException
{
    public CategoryNotFoundException(string name, object key):base(name,key)
    {
        
    }
}
