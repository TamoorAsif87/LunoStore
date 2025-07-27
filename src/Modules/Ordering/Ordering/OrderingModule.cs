using Ordering.Data.Seed;
using Shared.Data.Seed;

namespace Ordering;

public static class OrderingModule
{
    public static IServiceCollection AddOrderingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(
            config => { },
            typeof(OrderingModule).Assembly
        );

        // Data - Infrastructure related services

        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<OrderDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });


        services.AddScoped<IDataSeeder, OrderDBSeed>();

        return services;
    }

    public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
    {
        app.UseMigration<OrderDbContext>();
        return app;
    }
}
