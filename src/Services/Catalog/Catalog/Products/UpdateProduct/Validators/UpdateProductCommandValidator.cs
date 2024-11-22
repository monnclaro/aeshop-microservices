using FluentValidation;

namespace Catalog.Products.UpdateProduct.Validators;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name is required.").Length(2, 150).WithMessage("Name must be between 2 and 150 characters.");
        RuleFor(r => r.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
};