using Api.Converters;
using Identity;
using Shared.Messaging.Extensions;
using Shared.Services.CloudinaryService;
using Shared.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;
var identityAssembly = typeof(IdentityModule).Assembly;
var orderingAssembly = typeof(OrderingModule).Assembly;

builder.Services
    .AddMediatorWithAssembles(catalogAssembly, basketAssembly,identityAssembly,orderingAssembly);

builder.Services
    .AddMassTransitWithAssemblies(builder.Configuration,catalogAssembly, basketAssembly,orderingAssembly);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration)
    .AddIdentityModule(builder.Configuration);

builder.Services.AddScoped<IFileUpload,CloudinaryService>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseExceptionHandler(opt => { });


app.
    UseCatalogModule()
    .UseOrderingModule()
    .UseBasketModule()
    .UseIdentityModule();

app.UseCors("AllowAll");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
