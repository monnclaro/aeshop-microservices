using FluentValidation;

namespace Basket.Basket.DeleteBasket.Validations;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage("Id is required.");
    }
};