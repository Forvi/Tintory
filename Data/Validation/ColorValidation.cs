using COLOR.Domain.Entities;
using FluentValidation;

namespace COLOR.Data.Validation;

public class ColorValidation : AbstractValidator<ColorEntity>
{
    public ColorValidation()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("ID cannot be null")
            .NotEmpty().WithMessage("ID cannot be empty");

        RuleFor(c => c.HexCode)
            .NotNull().WithMessage("HexCode cannot be null")
            .NotEmpty().WithMessage("HexCode cannot be empty")
            .Matches("^[#0-9A-Z]+$").WithMessage("Incorrect code format")
            .Length(7).WithMessage("The code consists of 7 characters including '#'");
    }
}