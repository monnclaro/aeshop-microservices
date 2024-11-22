using BuildingBlocks.CQRS.Queries;
using Catalog.Exceptions;
using Catalog.Models;

namespace Catalog.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);   

internal class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly IDocumentSession _documentSession;

    public GetProductByIdHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _documentSession.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }
        
        return new GetProductByIdResult(product);
    }
}