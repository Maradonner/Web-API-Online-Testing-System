using FluentValidation;
using TestingSystem.Models;

namespace TestingSystem.Validators;

public class TriviaQuizValidator : AbstractValidator<TriviaQuiz>
{
    public TriviaQuizValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.QuestionTime)
            .GreaterThanOrEqualTo(1).WithMessage("Question time must be at least 1 second.");

        RuleFor(x => x.LivesCount)
            .GreaterThanOrEqualTo(1).When(x => x.LivesCount.HasValue)
            .WithMessage("Lives count must be at least 1.");

        RuleFor(x => x.Questions)
            .NotNull().WithMessage("Questions list is required.")
            .Must(x => x.Count > 0).WithMessage("At least one question is required.");
    }
}

public class TriviaQuizDTOValidator : AbstractValidator<QuizDto>
{
    public TriviaQuizDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.QuestionTime)
            .GreaterThanOrEqualTo(1).WithMessage("Question time must be at least 1 second.");

        RuleFor(x => x.LivesCount)
            .GreaterThanOrEqualTo(1).WithMessage("Lives count must be at least 1.");

        RuleForEach(x => x.Questions)
            .SetValidator(new TriviaQuestionDTOValidator());
    }
}