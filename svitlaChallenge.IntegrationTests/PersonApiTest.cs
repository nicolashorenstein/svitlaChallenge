using Microsoft.AspNetCore.Mvc.Testing;
using svitlaChallenge.Domain.Interfaces;


namespace svitlaChallenge.IntegrationTests
{
    public class PersonApiTests : IClassFixture<WebApplicationFactory<API.Program>>
    {
        private readonly HttpClient _client;
       
        public PersonApiTests(WebApplicationFactory<API.Program> factory)
        {
            _client = factory.CreateClient();  // Create HttpClient to interact with the API
        }

        [Fact]
        public async Task GetAllPersons_ReturnsSuccessStatusCode()
        {
            // Act
            var response = await _client.GetAsync("/api/person");

            // Assert
            response.EnsureSuccessStatusCode();  // Check that the status code is 2xx
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
        }
    }
}
