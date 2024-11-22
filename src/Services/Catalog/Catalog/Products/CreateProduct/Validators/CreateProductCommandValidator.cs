using FluentValidation;

namespace Catalog.Products.CreateProduct.Validators;

public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(r => r.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(r => r.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
        RuleFor(r => r.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(r => r.Categories).NotEmpty().WithMessage("A category is required.");
    }
}