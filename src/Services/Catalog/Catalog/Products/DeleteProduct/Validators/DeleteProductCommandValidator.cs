using FluentValidation;

namespace Catalog.Products.DeleteProduct.Validators;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage("Id is required.");
    }
};