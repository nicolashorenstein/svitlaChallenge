using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using svitlaChallenge.Application.Persons.Queries;
using svitlaChallenge.Domain.Interfaces;
using svitlaChallenge.Domain.Models;

namespace svitlaChallenge.Tests;

public class GetAllPersonsHandlerTests
{
    private readonly GetAllPersonsHandler _handler;
    private readonly Mock<ILogger<GetAllPersonsHandler>> _mockLogger;
    private readonly Mock<IPersonService> _mockPersonService;

    public GetAllPersonsHandlerTests()
    {
        _mockPersonService = new Mock<IPersonService>();
        _mockLogger = new Mock<ILogger<GetAllPersonsHandler>>();
        _handler = new GetAllPersonsHandler(_mockPersonService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Handle_ReturnsAllPersons()
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

        _mockPersonService.Setup(service => service.GetAllPeople()).ReturnsAsync(persons);

        var query = new GetAllPersonsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Persons.Should().BeEquivalentTo(persons);
    }
}