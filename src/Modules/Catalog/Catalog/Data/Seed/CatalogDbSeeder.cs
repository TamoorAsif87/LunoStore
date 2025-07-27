namespace Catalog.Data.Seed;

public class CatalogDbSeeder(CatalogDbContext context) : IDataSeeder
{
    public async Task SeedAllAsync()
    {
        if(context.Database.GetPendingMigrations().Count() == 0)
        {
            if (!context.Categories.Any())
            {
                await context.Categories.AddRangeAsync(InitialData.Categories);
                await context.SaveChangesAsync();
            }
        }

        if (context.Database.GetPendingMigrations().Count() == 0 && context.Categories.Any())
        {
            if (!context.Products.Any())
            {
                await context.Products.AddRangeAsync(InitialData.Products);
                await context.SaveChangesAsync();
            }
        }
    }
}
