using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using svitlaChallenge.Application.Persons.Queries;
using svitlaChallenge.Domain.Interfaces;
using svitlaChallenge.Domain.Models;

namespace svitlaChallenge.Tests;

public class GetPersonByIdHandlerTests
{
    private readonly GetPersonByIdHandler _handler;
    private readonly Mock<ILogger<GetPersonByIdHandler>> _mockLogger;
    private readonly Mock<IPersonService> _mockPersonService;
    private readonly Mock<IValidator<GetPersonByIdQuery>> _mockValidator;

    public GetPersonByIdHandlerTests()
    {
        _mockPersonService = new Mock<IPersonService>();
        _mockLogger = new Mock<ILogger<GetPersonByIdHandler>>();
        _mockValidator = new Mock<IValidator<GetPersonByIdQuery>>();
        _handler = new GetPersonByIdHandler(_mockPersonService.Object, _mockValidator.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Handle_ReturnsPersonById()
    {
        // Arrange
        var persons = new List<Person>
        {
            new()
            {
                Id = Guid.Parse("b1efe29f-abb7-477f-9cf0-76b2eae52861"),
                GivenName = "Nicolas",
                SurName = "Horenstein"
            },
            new()
            {
                Id = Guid.Parse("98b98813-01f5-41da-a91b-c9fcb6054ca8"),
                GivenName = "Julieta",
                SurName = "Simons"
            }
        };

        var query = new GetPersonByIdQuery { PersonId = Guid.Parse("b1efe29f-abb7-477f-9cf0-76b2eae52861") };

        // Mock the validator to succeed
        _mockValidator.Setup(v => v.ValidateAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mockPersonService.Setup(service => service.GetPersonById(Guid.Parse("b1efe29f-abb7-477f-9cf0-76b2eae52861")))
            .ReturnsAsync(persons[0]);


        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Persons[0].Should().BeEquivalentTo(persons[0]);
    }
}