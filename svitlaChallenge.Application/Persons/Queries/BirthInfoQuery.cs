using MediatR;
using svitlaChallenge.Application.Persons.Commands.Persons;
using svitlaChallenge.Domain.Results;

namespace svitlaChallenge.Application.Persons.Queries;

public class BirthInfoQuery : IRequest<BaseResult>
{
    public Guid PersonId { get; set; }
    public BirthInfoCommand Command { get; set; }
}