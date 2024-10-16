using MediatR;
using svitlaChallenge.Application.Students.Commands.Students;
using svitlaChallenge.Domain.Results;

namespace svitlaChallenge.Application.Students.Queries;

public class BirthInfoQuery : IRequest<BaseResult>
{
    public Guid PersonId { get; set; }
    public BirthInfoCommand Command { get; set; }
}