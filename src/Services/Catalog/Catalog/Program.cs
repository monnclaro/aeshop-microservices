var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

//Marten
builder.Services.AddMarten(o =>
{
    o.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);
}).UseLightweightSessions();

//Mediatr
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
      
var app = builder.Build();
app.MapCarter();

app.Run();