using BuildingBlocks.CQRS.Queries;
using Catalog.Exceptions;
using Catalog.Models;

namespace Catalog.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);   

internal class GetProductByCategoryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await documentSession.Query<Product>()
            .Where(l => l.Categories.Contains(query.Category))
            .ToListAsync();
        
        return new GetProductByCategoryResult(products);
    }
}