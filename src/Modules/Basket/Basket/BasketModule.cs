using Basket.Data.Processors;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interceptors;

namespace Basket;

public static class BasketModule
{
    public static IServiceCollection AddBasketModule(this IServiceCollection services,IConfiguration configuration)
    {
        // use case services

        services.AddAutoMapper(
            config => { },
            typeof(BasketModule).Assembly
           
            );

        // Data - Infrastructure related services

        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<BasketContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });


        services.AddScoped<IBasketRepository, BasketRepository>();
        services.Decorate<IBasketRepository,CacheBasketRepository>();

        services.AddHostedService<OutboxProcessor>();

        return services;
    }

    public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
    {
        app.UseMigration<BasketContext>();
        return app;
    }
}
