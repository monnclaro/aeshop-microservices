namespace Catalog.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Categories) : IRequest;
public record UpdateProductResponse(bool Updated);   

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("UpdateProduct")
            .WithDescription("UpdateProduct");
    }
}