using MediatR;
using svitlaChallenge.Application.Persons.Responses;

namespace svitlaChallenge.Application.Persons.Queries;

public class GetPersonByIdQuery : IRequest<PersonsResponse>
{
    public Guid PersonId { get; set; }
}