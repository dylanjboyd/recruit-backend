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

        [TestCase("A336557201063420", Description = "Letters")]
        [TestCase("@33655720106342_", Description = "Symbols")]
        [TestCase(" 336557201063420", Description = "Whitespace")]
        public void RegisterCard_ThrowsException_CardNumberContainsInvalidCharacters(string cardNumber)
        {
            // Given card with invalid character(s) in card number
            _card.CardNumber = cardNumber;
            
            // When registering the card
            var exception = Assert.Throws<ArgumentException>(() => _cardService.RegisterCard(_card));
            
            // Then an exception should be raised about the invalid card number
            Assert.AreEqual(exception.Message, CardConstants.CardErrorOnlyNumbers);
        }

        [TestCase(13, 2021, Description = "Month too large")]
        public void RegisterCard_ThrowsException_ExpiryDateInvalid(int expiryMonth, int expiryYear)
        {
            // Given card with invalid expiry month and/or year
            _card.ExpiryMonth = expiryMonth;
            _card.ExpiryYear = expiryYear;
            
            // When registering the card
            var exception = Assert.Throws<ArgumentException>(() => _cardService.RegisterCard(_card));
            
            // Then an exception should be raised about the invalid expiry
            Assert.AreEqual(exception.Message, CardConstants.CardErrorInvalidExpiry);
        }
    }
}