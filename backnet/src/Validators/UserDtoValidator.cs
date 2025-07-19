
using FluentValidation;

public class CreateUserValidator : AbstractValidator<CreateUserDto> 
{
  public CreateUserValidator()
  {
    RuleFor(user => user.Email)
      .NotEmpty().WithMessage("Email is required.")
      .EmailAddress().WithMessage("Invalid email format.");

    RuleFor(user => user.Password)
      .NotEmpty().WithMessage("Password is required.")
      .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
      .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
      .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$")
      .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one digit.");

    RuleFor(user => user.Name)
      .NotEmpty().WithMessage("Name is required.")
      .MinimumLength(2).WithMessage("Name cannot be 2 or less characters.");

    RuleFor(user => user.Country)
      .NotEmpty().WithMessage("Country is required.")
      .MaximumLength(100).WithMessage("Country cannot exceed 100 characters.");
      
    RuleFor(user => user.PhoneNumber)
      .NotEmpty().WithMessage("Phone number is required.")
      .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be in a valid format (e.g., +1234567890).")
      .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.");
  }
}

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
  public UpdateUserValidator()
  {
    RuleFor(user => user.Email)
      .NotEmpty().WithMessage("Email is required.")
      .EmailAddress().WithMessage("Invalid email format.");

    RuleFor(user => user.Name)
      .NotEmpty().WithMessage("Name is required.")
      .MinimumLength(2).WithMessage("Name cannot be 2 or less characters.");

    RuleFor(user => user.Country)
      .NotEmpty().WithMessage("Country is required.")
      .MaximumLength(100).WithMessage("Country cannot exceed 100 characters.");
      
    RuleFor(user => user.PhoneNumber)
      .NotEmpty().WithMessage("Phone number is required.")
      .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be in a valid format (e.g., +1234567890).")
      .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.");
  }
}