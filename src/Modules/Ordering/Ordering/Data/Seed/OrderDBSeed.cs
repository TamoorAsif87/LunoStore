using Shared.Data.Seed;

namespace Ordering.Data.Seed;

public class OrderDBSeed(OrderDbContext _context) : IDataSeeder
{
    public async Task SeedAllAsync()
    {
        if (!_context.Database.GetPendingMigrations().Any())
        {
            if (!_context.Orders.Any())
            {
                _context.Orders.AddRange(OrderInitialData.Orders);
                await _context.SaveChangesAsync();
            }
        }
    }
}
