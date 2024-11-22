using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.CustomExceptionHandler;
using Catalog.Infrastructure.Seeders;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

//Marten
builder.Services.AddMarten(o =>
{
    o.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.InitializeMartenWith<CatalogInitialDataSeeder>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

//Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Mediatr
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssembly(typeof(Program).Assembly);
    c.AddOpenBehavior(typeof(ValidationBehavior<,>));
    c.AddOpenBehavior(typeof(LogginBehavior<,>));
});
      
var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(o => {});
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();