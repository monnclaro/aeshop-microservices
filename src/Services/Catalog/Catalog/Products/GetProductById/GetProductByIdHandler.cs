using BuildingBlocks.CQRS.Queries;
using Catalog.Exceptions;
using Catalog.Models;

namespace Catalog.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);   

internal class GetProductByIdHandler(IDocumentSession documentSession) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }
        
        return new GetProductByIdResult(product);
    }
}