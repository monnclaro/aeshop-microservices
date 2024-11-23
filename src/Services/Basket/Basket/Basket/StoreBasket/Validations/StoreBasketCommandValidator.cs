using FluentValidation;

namespace Basket.Basket.StoreBasket.Validations;

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(r => r.ShoppingCart).NotNull().WithMessage("Cart cannot be null.");
        RuleFor(r => r.ShoppingCart.Username).NotEmpty().WithMessage("Username is required.");
    }
};