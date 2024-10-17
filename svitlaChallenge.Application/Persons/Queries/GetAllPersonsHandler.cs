using System.Net;
using MediatR;
using Microsoft.Extensions.Logging;
using svitlaChallenge.Application.Persons.Responses;
using svitlaChallenge.Domain.Interfaces;

namespace svitlaChallenge.Application.Persons.Queries;

public class GetAllPersonsHandler : IRequestHandler<GetAllPersonsQuery, PersonsResponse>
{
    private readonly ILogger<GetAllPersonsHandler> _logger;
    private readonly IPersonService _personService;

    public GetAllPersonsHandler(IPersonService personService, ILogger<GetAllPersonsHandler> logger)
    {
        _personService = personService;
        _logger = logger;
    }

    public async Task<PersonsResponse> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllPersonsQuery request.");

        var result = new PersonsResponse();

        try
        {
            var persons = await _personService.GetAllPeople();

            result.Persons = persons;
            _logger.LogInformation("Successfully retrieved {Count} persons.", persons.Count);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error when trying to retrieve all persons registered. Error: {error}", ex.Message);
            result.SetError(ex.Message, HttpStatusCode.InternalServerError);
            return result;
        }
    }
}