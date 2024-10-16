using MediatR;
using svitlaChallenge.Application.Persons.Commands.Persons;
using svitlaChallenge.Domain.Results;

namespace svitlaChallenge.Application.Persons.Queries;

public class AddPersonQuery : IRequest<BaseResult>
{
    public AddPersonCommand Command { get; set; }
}