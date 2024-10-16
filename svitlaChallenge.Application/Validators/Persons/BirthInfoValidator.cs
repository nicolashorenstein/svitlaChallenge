using FluentValidation;
using svitlaChallenge.Application.Students.Queries;

namespace svitlaChallenge.Application.Validators.Students
{
    public class BirthInfoValidator : AbstractValidator<BirthInfoQuery>
    {
        public BirthInfoValidator()
        {
            RuleFor(x => x.Command.BirthLocation)
                .NotNull()
                .WithMessage("The BirthLocation field shouldn't be null.");
            RuleFor(x => x.Command.BirthLocation)
                .NotNull()
                .Must(x => x.Length > 0)
                .WithMessage("The BirthLocation field shouldn't be empty.");
        }
    }
}