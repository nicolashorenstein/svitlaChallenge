using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using svitlaChallenge.Application.Students.Commands.Students;
using svitlaChallenge.Application.Students.Queries;
using svitlaChallenge.Domain.Interfaces;
using svitlaChallenge.Domain.Models;
using Xunit;

namespace svitlaChallenge.Tests;

public class UpdateBirhtIntoHandlerTests
{
    private readonly Mock<IPersonService> _mockPersonService;
    private readonly Mock<ILogger<BirthInfoHandler>> _mockLogger;
    private readonly Mock<IValidator<BirthInfoQuery>> _mockValidator;

    private readonly BirthInfoHandler _handler;

    public UpdateBirhtIntoHandlerTests()
    {
        _mockPersonService = new Mock<IPersonService>();
        _mockLogger = new Mock<ILogger<BirthInfoHandler>>();
        _mockValidator = new Mock<IValidator<BirthInfoQuery>>();
        _handler = new BirthInfoHandler(_mockPersonService.Object, _mockValidator.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_UpdatesBirthInfo()
    {
        // Arrange
        var command = new BirthInfoCommand
        {
            BirthDate = new DateTime(1988, 4, 27),
            BirthLocation = "Cordoba"
        };

        var addQuery = new BirthInfoQuery
        {
            PersonId = Guid.Parse("b1efe29f-abb7-477f-9cf0-76b2eae52861"),
            Command = command
        };

        var person = new Person
        {
            Id = addQuery.PersonId,
            BirthDate = new DateTime(1980, 1, 1),
            BirthLocation = "Old Location"
        };

        _mockPersonService.Setup(service => service.GetPersonById(addQuery.PersonId))
                          .ReturnsAsync(person);

        _mockValidator.Setup(v => v.ValidateAsync(addQuery, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new ValidationResult()); // Mocking successful validation


        _mockPersonService.Setup(service => service.UpdateBirthInfo(
         It.IsAny<Guid>(),
         It.IsAny<DateTime>(),
         It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        await _handler.Handle(addQuery, CancellationToken.None);

        // Assert
        // Verify that UpdateBirthInfo was called with the correct parameters
        _mockPersonService.Verify(service => service.UpdateBirthInfo(
            person.Id,
            addQuery.Command.BirthDate,
            addQuery.Command.BirthLocation), Times.Once);
        _mockPersonService.Verify(service => service.UpdateBirthInfo(person.Id, addQuery.Command.BirthDate, addQuery.Command.BirthLocation), Times.Once);
    }
}