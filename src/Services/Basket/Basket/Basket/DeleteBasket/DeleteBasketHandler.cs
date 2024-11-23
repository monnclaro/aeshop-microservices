using BuildingBlocks.CQRS.Commands;

namespace Basket.Basket.DeleteBasket;

public record DeleteBasketCommand(Guid Id) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool Success);

public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}