using BuildingBlocks.CQRS.Commands;
using Catalog.Exceptions;
using Catalog.Models;

namespace Catalog.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Categories) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool Success);   

internal class UpdateProductHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IDocumentSession _documentSession;
    
    public UpdateProductHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }
    
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _documentSession.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Update(command);
        _documentSession.Update(product);
        await _documentSession.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}