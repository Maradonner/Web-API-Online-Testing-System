using FluentValidation;
using TestingSystem.Models;

namespace TestingSystem.Validators;

public class TriviaOptionValidator : AbstractValidator<TriviaOption>
{
    public TriviaOptionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Option title is required.")
            .MinimumLength(3).WithMessage("Option title cannot be less than 3 characters.")
            .MaximumLength(255).WithMessage("Option title cannot exceed 255 characters.");
    }
}

