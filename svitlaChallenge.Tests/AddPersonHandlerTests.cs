using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using svitlaChallenge.Application.Persons.Commands.Persons;
using svitlaChallenge.Application.Persons.Queries;
using svitlaChallenge.Domain.Interfaces;
using svitlaChallenge.Domain.Models;
using Xunit;

namespace svitlaChallenge.Tests;

public class AddPersonHandlerTests
{
    private readonly Mock<IPersonService> _mockPersonService;
    private readonly Mock<ILogger<AddPersonHandler>> _mockLogger;
    private readonly Mock<IValidator<AddPersonQuery>> _mockValidator;

    private readonly AddPersonHandler _handler;

    public AddPersonHandlerTests()
    {
        _mockPersonService = new Mock<IPersonService>();
        _mockLogger = new Mock<ILogger<AddPersonHandler>>();
        _mockValidator = new Mock<IValidator<AddPersonQuery>>();
        _handler = new AddPersonHandler(_mockPersonService.Object, _mockValidator.Object, _mockLogger.Object);
    }
    
    [Fact]
    public async Task Handle_ReturnsPersonById()
    {
        // Arrange
        var command = new AddPersonCommand
        {
            GivenName = "Nicolas",
            SurName = "Horenstein",
            Gender = Gender.Male
        };

        var addQuery = new AddPersonQuery
        {
            Command = command
        };

        var personId = Guid.NewGuid();

        _mockValidator.Setup(v => v.ValidateAsync(addQuery, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new ValidationResult()); // Mocking successful validation


        _mockPersonService.Setup(service => service.AddPerson(It.IsAny<Person>()))
                          .Callback<Person>(p => p.Id = personId) // Set the ID of the person
                          .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(addQuery, CancellationToken.None);

        // Assert
        result.Ok.Should().Be(true);

        // Verify that the service method was called
        _mockPersonService.Verify(service => service.AddPerson(It.IsAny<Person>()), Times.Once);
    }
}