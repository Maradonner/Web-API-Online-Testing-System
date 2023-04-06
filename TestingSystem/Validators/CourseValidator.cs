using FluentValidation;
using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Validators
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(c => c.CourseCode)
                .NotEmpty().WithMessage("Course code is required.")
                .Length(4, 10).WithMessage("Course code must be between 4 and 10 characters.");

            RuleFor(c => c.TeacherId)
                .NotEmpty().WithMessage("Teacher ID is required.");
        }
    }

    public class CourseDtoValidator : AbstractValidator<CourseDto>
    {
        public CourseDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(2).WithMessage("Name must not exceed 2 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(c => c.CourseCode)
                .NotEmpty().WithMessage("Course code is required.")
                .Length(4, 10).WithMessage("Course code must be between 4 and 10 characters.");

            RuleFor(c => c.TeacherId)
                .NotEmpty().WithMessage("Teacher ID is required.");
        }
    }
}
