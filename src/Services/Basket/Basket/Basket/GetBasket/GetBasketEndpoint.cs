using Basket.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart ShoppingCart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string username, ISender sender ) =>
        {
            var result = await sender.Send(new GetBasketCommand(username));
            var response = result.Adapt<GetBasketResponse>();
            
            return Results.Ok(response);
        }) 
        .WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetBasket")
        .WithDescription("GetBasket");
    }
}