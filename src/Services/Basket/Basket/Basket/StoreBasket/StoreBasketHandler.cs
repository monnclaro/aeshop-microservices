using Basket.Models;
using BuildingBlocks.CQRS.Commands;

namespace Basket.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string Username);

public class StoreBasketHandler: ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}