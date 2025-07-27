using Ordering.ValueObjects;

namespace Ordering.Orders.Models;

public class Order:Aggregate<Guid>
{
    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    public string? CustomerId { get; private set; }
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingsAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public decimal TotalPrice => Items.Sum(item => item.Quantity * item.Price);

    public static Order Create(Guid id,string customerId,Address shippingAddress,Address billingAddress,Payment payment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            ShippingAddress = shippingAddress,
            BillingsAddress = billingAddress,
            Payment = payment

        };
        return order;
    }

    public void AddOrderItem(OrderItem item)
    {
        _items.Add(item);
    }

    public void ReplaceOrderItems(IEnumerable<OrderItem> items)
    {
        _items.Clear();
        _items.AddRange(items);
    }

    public void UpdateShippingAddress(Address newAddress)
    {
        ShippingAddress = newAddress;
    }

    public void UpdateBillingAddress(Address newAddress)
    {
        BillingsAddress = newAddress;
    }

    public void UpdatePayment(Payment newPayment)
    {
        Payment = newPayment;
    }
}
