using MediatR;
using svitlaChallenge.Application.Persons.Responses;

namespace svitlaChallenge.Application.Persons.Queries;

public class GetAllPersonsQuery : IRequest<PersonsResponse>
{
}