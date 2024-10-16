using FluentValidation;
using svitlaChallenge.Application.Students.Queries;

namespace svitlaChallenge.Application.Validators.Students
{
    public class AddPersonValidator : AbstractValidator<AddPersonQuery>
    {
        public AddPersonValidator()
        {
            RuleFor(x => x.Command.GivenName)
                .NotNull()
                .Must(x => x.Length > 0)
                .WithMessage("The GivenName field shouldn't be empty.");
            RuleFor(x => x.Command.SurName)
                .NotNull()
                .Must(x => x.Length > 0)
                .WithMessage("The SurName field shouldn't be empty.");
            RuleFor(x => x.Command.Gender)
                .NotNull()
                .WithMessage("The Gender field shouldn't be null.");
        }
    }
}