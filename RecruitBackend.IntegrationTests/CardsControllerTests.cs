using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using RecruitBackend.Models;
using Xunit;

namespace RecruitBackend.IntegrationTests
{
    public class CardsControllerTests : IntegrationTest, IClassFixture<TestWebApplicationFactory>
    {
        public CardsControllerTests(TestWebApplicationFactory appFactory) : base(appFactory)
        {
        }

        [Fact]
        public async Task GetAllCards_EmptyResponse_NoCards()
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync("cards");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Card>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllCards_ContainsCards_RepositoryHasCards()
        {
            // Arrange
            await using (var context = GetDatabaseContext())
            {
                await context.Cards.AddRangeAsync(new[]
                {
                    new Card
                    {
                        Name = "IVETTE H LITTLEFIELD",
                        CardNumber = "5297106060264732",
                        ExpiryMonth = 9,
                        ExpiryYear = 2024,
                        CVC = 221
                    },
                    new Card
                    {
                        Name = "LLOYD A KAIN",
                        CardNumber = "5163767519441725",
                        ExpiryMonth = 9,
                        ExpiryYear = 2025,
                        CVC = 677
                    }
                });

                await context.SaveChangesAsync();
            }

            // Act
            var response = await TestClient.GetAsync("cards");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Card>>()).Should().HaveCount(2);
        }
    }
}