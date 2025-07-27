using AutoMapper.QueryableExtensions;

namespace Ordering.Orders.Features.GetAllOrders;


public record GetAllOrdersQuery : IQuery<List<OrderDto>>;
internal class GetAllOrdersQueryHandler(OrderDbContext dbContext, IMapper mapper)
    : IQueryHandler<GetAllOrdersQuery, List<OrderDto>>
{
    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
        .Include(o => o.Items)
        .ToListAsync(cancellationToken);

        return mapper.Map<List<OrderDto>>(orders);
    }
}
