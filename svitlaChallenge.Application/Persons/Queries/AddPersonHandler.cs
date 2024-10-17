using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using svitlaChallenge.Domain.Interfaces;
using svitlaChallenge.Domain.Models;
using svitlaChallenge.Domain.Results;

namespace svitlaChallenge.Application.Persons.Queries;

public class AddPersonHandler : IRequestHandler<AddPersonQuery, BaseResult>
{
    private readonly ILogger<AddPersonHandler> _logger;
    private readonly IPersonService _personService;
    private readonly IValidator<AddPersonQuery> _validator;

    public AddPersonHandler(IPersonService personService, IValidator<AddPersonQuery> validator,
        ILogger<AddPersonHandler> logger)
    {
        _personService = personService;
        _validator = validator;
        _logger = logger;
    }

    public async Task<BaseResult> Handle(AddPersonQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling AddPersonHandler request.");

        var result = new BaseResult();

        var validation = await _validator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            var errors = string.Join(Environment.NewLine, validation.Errors);
            result.SetError(errors, HttpStatusCode.BadRequest);
            return result;
        }

        try
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                GivenName = request.Command.GivenName,
                SurName = request.Command.SurName,
                Gender = request.Command.Gender,
                BirthDate = request.Command.BirthDate,
                BirthLocation = request.Command.BirthLocation,
                DeathDate = request.Command.DeathDate,
                DeathLocation = request.Command.DeathLocation
            };

            await _personService.AddPerson(person);
            result.SetHttpStatusCode(HttpStatusCode.Created, true);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error when trying register a new person. Error: {error}", ex.Message);
            result.SetError(ex.Message, HttpStatusCode.InternalServerError);
            return result;
        }
    }
}