using FluentValidation;
using TestingSystem.Models;

namespace TestingSystem.Validators;

public class TriviaQuestionValidator : AbstractValidator<TriviaQuestion>
{
    public TriviaQuestionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Question title is required.")
            .MaximumLength(500).WithMessage("Question title cannot exceed 500 characters.");

        RuleFor(x => x.Options)
            .NotNull().WithMessage("Options list is required.")
            .Must(x => x.Count >= 2).WithMessage("At least two options are required.")
            .Must(x => x.Count(o => o.IsCorrect) >= 1).WithMessage("One or more options should be marked as correct.");

        RuleForEach(x => x.Options)
            .SetValidator(new TriviaOptionValidator());
    }
}

public class TriviaQuestionDTOValidator : AbstractValidator<QuestionDto>
{
    public TriviaQuestionDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Question title is required.")
            .MaximumLength(500).WithMessage("Question title cannot exceed 500 characters.");

        RuleForEach(x => x.Options)
            .SetValidator(new TriviaOptionDTOValidator());
    }
}