using Basket.Models;
using BuildingBlocks.CQRS.Queries;

namespace Basket.Basket.GetBasket;

public record GetBasketCommand(string Username) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart ShoppingCart);

public class GetBasketHandler : IQueryHandler<GetBasketCommand, GetBasketResult>
{
    public Task<GetBasketResult> Handle(GetBasketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}