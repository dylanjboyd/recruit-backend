using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using RecruitBackend.Models;
using Xunit;

namespace RecruitBackend.IntegrationTests
{
    public class CardsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAllCards_EmptyResponse_NoCards()
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync("cards");

            // Assert
            // response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsAsync<List<Card>>();
            content.Should().BeEmpty();
        }
    }
}