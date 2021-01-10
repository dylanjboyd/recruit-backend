using System.Collections.Generic;
using System.Linq;
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
        private readonly Card _cardIvette = new()
        {
            Name = "IVETTE H LITTLEFIELD",
            CardNumber = "5297106060264732",
            ExpiryMonth = 9,
            ExpiryYear = 2024,
            CVC = 221
        };

        private readonly Card _cardLloyd = new()
        {
            Name = "LLOYD A KAIN",
            CardNumber = "5163767519441725",
            ExpiryMonth = 9,
            ExpiryYear = 2025,
            CVC = 677
        };

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
                    _cardIvette,
                    _cardLloyd
                });

                await context.SaveChangesAsync();
            }

            // Act
            var response = await TestClient.GetAsync("cards");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Card>>()).Should().HaveCount(2);
        }

        [Fact]
        public async Task RegisterCard_Success_CardDetailsValid()
        {
            // Arrange
            await using (var context = GetDatabaseContext())
            {
                await context.ValidCards.AddAsync(new ValidCard
                {
                    CardNumber = _cardIvette.CardNumber
                });
                await context.SaveChangesAsync();
            }

            // Act
            var response = await TestClient.PostAsJsonAsync("cards", _cardIvette);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var cards = GetDatabaseContext().Cards;
            cards.Should().HaveCount(1);
            cards.First().Should().BeEquivalentTo(_cardIvette);
        }
    }
}