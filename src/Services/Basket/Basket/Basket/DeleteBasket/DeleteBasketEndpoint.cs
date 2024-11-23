using Carter;
using Mapster;
using MediatR;

namespace Basket.Basket.DeleteBasket;

public record DeleteBasketResponse(bool Success);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(id));
                var response = result.Adapt<DeleteBasketResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("DeleteBasket")
            .WithDescription("DeleteBasket");
    }
}