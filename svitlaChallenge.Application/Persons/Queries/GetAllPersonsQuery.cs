using MediatR;
using svitlaChallenge.Application.Students.Responses;

namespace svitlaChallenge.Application.Students.Queries;

public class GetAllPersonsQuery : IRequest<PersonsResponse>
{
}