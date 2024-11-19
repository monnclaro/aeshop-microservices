using BuildingBlocks.CQRS.Commands;
using Catalog.Exceptions;
using Catalog.Models;

namespace Catalog.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool Deleted);   

internal class DeleteProductHandler(IDocumentSession documentSession) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }
        
        documentSession.Delete(product);
        await documentSession.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}