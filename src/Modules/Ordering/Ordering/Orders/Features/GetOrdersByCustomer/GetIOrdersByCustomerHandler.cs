namespace Ordering.Orders.Features.GetOrdersByCustomer;


public record GetOrdersByCustomerIdQuery(string CustomerId) : IQuery<List<OrderDto>>;

internal class GetOrdersByCustomerIdQueryHandler(OrderDbContext dbContext, IMapper mapper)
    : IQueryHandler<GetOrdersByCustomerIdQuery, List<OrderDto>>
{
    public async Task<List<OrderDto>> Handle(GetOrdersByCustomerIdQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.Items)
            .Where(o => o.CustomerId == query.CustomerId)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<OrderDto>>(orders);
    }
}
