namespace Basket.Basket.Exceptions;
internal class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string userName):base("Basket", userName)
    {
    }

    
}