using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using RecruitBackend.Models;

namespace RecruitBackend.Services
{
    public interface ICardService
    {
        /// <summary>
        /// Validate and register a card.
        /// </summary>
        /// <param name="cardForCreation">The valid card to be created.</param>
        /// <returns>The created card along with any further populated fields.</returns>
        public Card RegisterCard(Card cardForCreation);
    }

    public class CardService : ICardService
    {
        private readonly ILogger<CardService> _logger;

        public CardService(ILogger<CardService> logger)
        {
            _logger = logger;
        }
        public Card RegisterCard(Card cardForCreation)
        {
            if (Regex.IsMatch(cardForCreation.CardNumber, @"[a-zA-Z\W_]"))
            {
                throw new ArgumentException(CardConstants.CardErrorOnlyNumbers);
            }

            if (cardForCreation.ExpiryMonth > 12)
            {
                throw new ArgumentException(CardConstants.CardErrorInvalidExpiry);
            }
            return cardForCreation;
        }
    }
}