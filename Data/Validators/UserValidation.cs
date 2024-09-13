using COLOR.Domain.Entities;
using FluentValidation;

namespace COLOR.Data.Validation;

public class UserValidation : AbstractValidator<UserEntity>
{
    public UserValidation()
    {
        RuleFor(u => u.Id)
            .NotNull().WithMessage("ID cannot be null")
            .NotEmpty().WithMessage("ID cannot be empty");
        
        RuleFor(u => u.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .Matches("^[0-9a-zA-Zа-яА-Я_]+$").WithMessage("Name contains invalid characters.")
            .MinimumLength(2).WithMessage("Name cannot be less than 2 characters")
            .MaximumLength(24).WithMessage("Name cannot be more than 24 characters");

        RuleFor(u => u.Email)
            .NotNull().WithMessage("Email cannot be null")
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Invalid email address");
    }

    public bool ValidatePassword(string password)
    {
        if (password.Length < 5)
            return false;
        else if (password.Length > 100)
            return false;
        return true;
    }
}