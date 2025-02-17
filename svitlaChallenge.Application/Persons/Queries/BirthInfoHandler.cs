﻿using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using svitlaChallenge.Domain.Interfaces;
using svitlaChallenge.Domain.Results;

namespace svitlaChallenge.Application.Persons.Queries;

public class BirthInfoHandler : IRequestHandler<BirthInfoQuery, BaseResult>
{
    private readonly ILogger<BirthInfoHandler> _logger;
    private readonly IPersonService _personService;
    private readonly IValidator<BirthInfoQuery> _validator;

    public BirthInfoHandler(IPersonService personService, IValidator<BirthInfoQuery> validator,
        ILogger<BirthInfoHandler> logger)
    {
        _personService = personService;
        _validator = validator;
        _logger = logger;
    }

    public async Task<BaseResult> Handle(BirthInfoQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling BirthInfoHandler request.");

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
            var updateResult = await _personService.UpdateBirthInfo(request.PersonId, request.Command.BirthDate,
                request.Command.BirthLocation);

            if (updateResult == false)
            {
                result.SetError("Person not found", HttpStatusCode.NotFound);

                return result;
            }

            result.SetHttpStatusCode(HttpStatusCode.NoContent, true);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error when trying to update birth info: {error}", ex.Message);
            result.SetError(ex.Message, HttpStatusCode.InternalServerError);
            return result;
        }
    }
}