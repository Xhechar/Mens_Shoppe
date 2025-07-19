
using FluentValidation;

public class ProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Product description is required.")
            .MaximumLength(1000).WithMessage("Product description cannot exceed 1000 characters.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");

        RuleFor(p => p.Type)
            .NotEmpty().WithMessage("Product type is required.")
            .MaximumLength(50).WithMessage("Product type cannot exceed 50 characters.");

        RuleFor(p => p.Size)
            .NotEmpty().WithMessage("Product size is required.")
            .MaximumLength(50).WithMessage("Product size cannot exceed 50 characters.");

        RuleFor(p => p.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Product quantity cannot be negative.");
        
        RuleFor(p => p.StockLimit)
            .GreaterThanOrEqualTo(0).WithMessage("Product stock limit cannot be negative.");

        RuleFor(p => p.Images)
            .NotEmpty().WithMessage("Product images are required.")
            .Must(images => images.Split(',').Length <= 8)
            .WithMessage("You can upload a maximum of 8 images.");
    }
}