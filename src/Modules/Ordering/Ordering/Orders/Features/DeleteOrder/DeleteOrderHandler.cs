using Ordering.Orders.Exceptions;

namespace Ordering.Orders.Features.DeleteOrder;


public record DeleteOrderCommand(Guid OrderId) : ICommand;

internal class DeleteOrderHandler(OrderDbContext dbContext) : ICommandHandler<DeleteOrderCommand>
{
    public async Task<Unit> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == command.OrderId, cancellationToken);

        if (order is null)
            throw new OrderNotFoundException(command.OrderId);

        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
