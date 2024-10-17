using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using svitlaChallenge.Application.Persons.Responses;
using svitlaChallenge.Domain.Interfaces;

namespace svitlaChallenge.Application.Persons.Queries;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonsResponse>
{
    private readonly ILogger<GetPersonByIdHandler> _logger;
    private readonly IPersonService _personService;
    private readonly IValidator<GetPersonByIdQuery> _validator;

    public GetPersonByIdHandler(IPersonService personService, IValidator<GetPersonByIdQuery> validator,
        ILogger<GetPersonByIdHandler> logger)
    {
        _personService = personService;
        _validator = validator;
        _logger = logger;
    }

    public async Task<PersonsResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetPersonByIdHandler request.");
        var result = new PersonsResponse();

        var validation = await _validator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            var errors = string.Join(Environment.NewLine, validation.Errors);
            result.SetError(errors, HttpStatusCode.BadRequest);
            return result;
        }

        try
        {
            var person = await _personService.GetPersonById(request.PersonId);

            if (person != null)
            {
                result.Persons.Add(person);

                return result;
            }

            result.SetHttpStatusCode(HttpStatusCode.NotFound, true);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error when trying to get person by id. Error: {error}", ex.Message);
            result.SetError(ex.Message, HttpStatusCode.InternalServerError);
            return result;
        }
    }
}