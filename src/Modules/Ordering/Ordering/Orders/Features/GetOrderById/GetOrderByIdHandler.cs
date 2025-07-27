namespace Ordering.Orders.Features.GetOrderById;

public record GetOrderByIdQuery(Guid OrderId) : IQuery<OrderDto>;
internal class GetOrderByIdHandler(OrderDbContext dbContext, IMapper mapper)
    : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == query.OrderId, cancellationToken);

        if (order is null)
            throw new OrderNotFoundException(query.OrderId);

        return mapper.Map<OrderDto>(order);
    }
}
