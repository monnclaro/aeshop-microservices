using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.CustomExceptionHandler;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

//Marten
builder.Services.AddMarten(o =>
{
    o.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

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

app.Run();