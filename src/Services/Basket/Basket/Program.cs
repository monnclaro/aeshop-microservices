using BuildingBlocks.Behaviors;
using Carter;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

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