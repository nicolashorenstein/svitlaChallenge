using MediatR;
using svitlaChallenge.Application.Students.Responses;

namespace svitlaChallenge.Application.Students.Queries;

public class GetPersonByIdQuery : IRequest<PersonsResponse>
{
    public Guid PersonId { get; set; }
}