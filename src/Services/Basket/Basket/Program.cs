using BuildingBlocks.Behaviors;
using Carter;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

//Mediatr
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssembly(typeof(Program).Assembly);
    c.AddOpenBehavior(typeof(ValidationBehavior<,>));
    c.AddOpenBehavior(typeof(LogginBehavior<,>));
});

var app = builder.Build();
app.MapCarter();
app.Run();