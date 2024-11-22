using BuildingBlocks.CQRS.Queries;
using Catalog.Exceptions;
using Catalog.Models;

namespace Catalog.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);   

internal class GetProductByCategoryHandler : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    private readonly IDocumentSession _documentSession;
    
    public GetProductByCategoryHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }
    
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await _documentSession.Query<Product>()
            .Where(l => l.Categories.Contains(query.Category))
            .ToListAsync(token: cancellationToken);
        
        return new GetProductByCategoryResult(products);
    }
}