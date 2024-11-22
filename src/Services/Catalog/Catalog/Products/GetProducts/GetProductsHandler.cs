using BuildingBlocks.CQRS.Queries;
using Catalog.Models;

namespace Catalog.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);   

internal class GetProductsHandler : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    private readonly IDocumentSession _documentSession;
    
    public GetProductsHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }
    
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await _documentSession.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}