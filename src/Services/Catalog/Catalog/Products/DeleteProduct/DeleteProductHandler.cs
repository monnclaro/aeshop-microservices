using BuildingBlocks.CQRS.Commands;
using Catalog.Exceptions;
using Catalog.Models;

namespace Catalog.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool Success);   

internal class DeleteProductHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IDocumentSession _documentSession;
    
    public DeleteProductHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }
    
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _documentSession.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        
        _documentSession.Delete(product);
        await _documentSession.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}