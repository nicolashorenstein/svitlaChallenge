using MediatR;
using svitlaChallenge.Application.Students.Commands.Students;
using svitlaChallenge.Domain.Results;

namespace svitlaChallenge.Application.Students.Queries;

public class AddPersonQuery : IRequest<BaseResult>
{
    public AddPersonCommand Command { get; set; }
}