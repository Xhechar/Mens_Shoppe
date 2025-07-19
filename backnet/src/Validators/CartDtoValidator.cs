
using FluentValidation;

public class CartDtoValidator : AbstractValidator<CreateCartDto> 
{
    public CartDtoValidator()
    {
        // RuleFor(c => c.ProductId)
        //     .NotEmpty().WithMessage("Product ID is required.")
        //     .MaximumLength(50).WithMessage("Product ID cannot exceed 50 characters.");

        RuleFor(c => c.Quantity)
            .NotEmpty().WithMessage("Quantity is required.")
            .InclusiveBetween(1, int.MaxValue).WithMessage("Quantity must be a positive integer.")
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}