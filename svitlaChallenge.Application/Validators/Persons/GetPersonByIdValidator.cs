using FluentValidation;
using svitlaChallenge.Application.Students.Queries;

namespace svitlaChallenge.Application.Validators.Students
{
    public class GetPersonByIdValidator : AbstractValidator<GetPersonByIdQuery>
    {
        public GetPersonByIdValidator()
        {
            RuleFor(x => x.PersonId)
                .NotNull()
                .WithMessage("The PersonId field shouldn't be null.");
        }
    }
}