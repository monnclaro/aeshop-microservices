using BuildingBlocks.CQRS.Command;
using BuildingBlocks.CQRS.Commands;
using Catalog.Models;

namespace Catalog.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, string ImageFile, decimal Price, List<string> Categories) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);   

internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product();
        product.Create(command);

        return new CreateProductResult(product.Id);
    }
}