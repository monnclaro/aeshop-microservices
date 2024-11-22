using BuildingBlocks.CQRS.Queries;
using Catalog.Models;
using Marten.Pagination;

namespace Catalog.Products.GetProducts;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 15) : IQuery<GetProductsResult>;
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
        var products = await _documentSession.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 15);
        return new GetProductsResult(products);
    }
}