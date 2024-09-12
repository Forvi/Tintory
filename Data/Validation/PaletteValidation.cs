using COLOR.Domain.Etities;
using FluentValidation;

namespace COLOR.Data.Validation;

public class PaletteValidation : AbstractValidator<PaletteEntity>
{
    public PaletteValidation()
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage("ID cannot be null")
            .NotEmpty().WithMessage("ID cannot be empty");
        
        RuleFor(p => p.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .Matches("^[0-9a-zA-Zа-яА-Я_\\s]+$").WithMessage("Name contains invalid characters.")
            .MinimumLength(2).WithMessage("Name cannot be less than 2 characters")
            .MaximumLength(24).WithMessage("Name cannot be more than 24 characters");
    }
}