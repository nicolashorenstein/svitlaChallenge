using FluentValidation;
using svitlaChallenge.Application.Persons.Queries;

namespace svitlaChallenge.Application.Validators.Persons;

public class GetPersonByIdValidator : AbstractValidator<GetPersonByIdQuery>
{
    public GetPersonByIdValidator()
    {
        RuleFor(x => x.PersonId)
            .NotNull()
            .WithMessage("The PersonId field shouldn't be null.");
    }
}