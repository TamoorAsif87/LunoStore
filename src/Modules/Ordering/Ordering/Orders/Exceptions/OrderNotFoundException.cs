using Shared.Exceptions;

namespace Ordering.Orders.Exceptions;

public class OrderNotFoundException:NotFoundException
{
    public OrderNotFoundException(object key):base("Order", key)
    {
        
    }
}
