using BuildingBlocks.CQRS.Commands;
using Catalog.Models;
using FluentValidation;

namespace Catalog.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, string ImageFile, decimal Price, List<string> Categories) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IDocumentSession _documentSession;
    
    public CreateProductHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }
        
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product();
        product.Create(command);
        
        _documentSession.Store(product);
        await _documentSession.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}