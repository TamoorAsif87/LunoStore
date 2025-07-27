using Identity.Account.Models;
using Identity.Data.Seed;
using Microsoft.Extensions.Options;
using Shared.Models;

namespace Identity;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        // mappers

        services.AddAutoMapper(
            config => { },
            typeof(IdentityModule).Assembly
            );

        // Application - Cloudinary related services

        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

        services.AddSingleton(sp =>
        {
            var config = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;
            var account = new CloudinaryDotNet.Account(
                config.CloudName,
                config.ApiKey,
                config.ApiSecret
                );

            return new CloudinaryDotNet.Cloudinary(account);
        });


        // Data - Infrastructure related services

        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<IdentityContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });


        services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
        {
            opt.Password.RequiredLength = 6;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireNonAlphanumeric = false;
        })
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("AdminProvider")
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime  = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        services.AddScoped<IDataSeeder, IdentityDBSeed>();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }

    public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
    {
        app.UseMigration<IdentityContext>();


        return app;
    }
}
