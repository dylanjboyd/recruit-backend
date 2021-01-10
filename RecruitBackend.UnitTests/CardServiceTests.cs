using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RecruitBackend.Models;
using RecruitBackend.Services;

namespace RecruitBackend.UnitTests
{
    public class CardServiceTests
    {
        private CardService _cardService;

        [SetUp]
        public void Setup()
        {
            var logger = new Mock<ILogger<CardService>>();
            _cardService = new CardService(logger.Object);
        }

        [Test]
        public void RegisterCard_ShouldWork_DetailsValid()
        {
            // Given valid card
            var card = new Card
            {
                Name = "LOUISE T WISE",
                CardNumber = "5336557201063420",
                CVC = 800,
                ExpiryMonth = 6,
                ExpiryYear = 2025
            };
            
            // When registering the card
            var createdCard = _cardService.RegisterCard(card);
            
            // Then the card should be created with no issues
            Assert.NotNull(createdCard);
        }
    }
}