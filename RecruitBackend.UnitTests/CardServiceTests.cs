using System;
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
        private Card _card;

        [SetUp]
        public void Setup()
        {
            var logger = new Mock<ILogger<CardService>>();
            _cardService = new CardService(logger.Object);
            _card = new Card
            {
                Name = "LOUISE T WISE",
                CardNumber = "5336557201063420",
                CVC = 800,
                ExpiryMonth = 6,
                ExpiryYear = 2025
            };
        }

        [Test]
        public void RegisterCard_ShouldWork_DetailsValid()
        {
            // Given valid card
            
            // When registering the card
            var createdCard = _cardService.RegisterCard(_card);
            
            // Then the card should be created with no issues
            Assert.NotNull(createdCard);
        }

        [Test]
        public void RegisterCard_ThrowsException_CardNumberContainsLetters()
        {
            // Given card with letter in card number
            _card.CardNumber = "A336557201063420";
            
            // When registering the card
            var exception = Assert.Throws<ArgumentException>(() => _cardService.RegisterCard(_card));
            
            // Then an exception should be raised about the invalid card number
            Assert.AreEqual(exception.Message, "CardNumber should only contain numbers.");
        }
    }
}