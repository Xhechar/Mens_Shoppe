
using FluentValidation;

public class ReviewDtoValidator : AbstractValidator<CreateReviewDto> 
{
    public ReviewDtoValidator()
    {
        RuleFor(r => r.Rating)
            .NotEmpty().WithMessage("Rating is required.")
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

        RuleFor(r => r.Comment)
            .NotEmpty().WithMessage("Comment is required.")
            .MinimumLength(10).WithMessage("Comment must be at least 10 characters long.")
            .MaximumLength(500).WithMessage("Comment cannot exceed 500 characters.");
    }
}