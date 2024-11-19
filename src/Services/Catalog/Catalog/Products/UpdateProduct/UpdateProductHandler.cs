using BuildingBlocks.CQRS.Commands;
using Catalog.Exceptions;
using Catalog.Models;

namespace Catalog.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Categories) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool Updated);   

internal class UpdateProductHandler(IDocumentSession documentSession) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        product.Update(command);
        documentSession.Update(product);
        await documentSession.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}