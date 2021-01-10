using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using RecruitBackend.Models;
using RecruitBackend.Repositories;

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

        IEnumerable<Card> GetAllCards();
        Card GetByCardNumber(string cardNumber);
    }

    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly ILogger<CardService> _logger;
        private readonly IValidCardRepository _validCardRepository;

        public CardService(ILogger<CardService> logger, ICardRepository cardRepository,
            IValidCardRepository validCardRepository)
        {
            _logger = logger;
            _cardRepository = cardRepository;
            _validCardRepository = validCardRepository;
        }

        public Card RegisterCard(Card cardForCreation)
        {
            ValidateCardNumberOrThrow(cardForCreation);
            ValidateCardExpiryOrThrow(cardForCreation);
            ValidateCardNameOrThrow(cardForCreation);
            ValidateCardInStorageOrThrow(cardForCreation);

            _cardRepository.Insert(cardForCreation);

            return cardForCreation;
        }

        public IEnumerable<Card> GetAllCards()
        {
            return _cardRepository.GetAll();
        }

        public Card GetByCardNumber(string cardNumber)
        {
            return _cardRepository.GetByCardNumber(cardNumber);
        }

        private void ValidateCardInStorageOrThrow(Card card)
        {
            if (_validCardRepository.GetByCardNumber(card.CardNumber) == null)
            {
                throw new ArgumentException(CardConstants.CardErrorNotInStorage);
            }
        }

        private static void ValidateCardNameOrThrow(Card card)
        {
            if (string.IsNullOrWhiteSpace(card.Name) || Regex.IsMatch(card.Name, @"[^\w\s]") ||
                card.Name.Length > 50)
            {
                throw new ArgumentException(CardConstants.CardErrorInvalidName);
            }
        }

        private static void ValidateCardExpiryOrThrow(Card card)
        {
            if (card.ExpiryMonth > 12 || card.ExpiryMonth < 1 ||
                card.ExpiryYear > DateTime.MaxValue.Year || card.ExpiryYear < DateTime.MinValue.Year)
            {
                throw new ArgumentException(CardConstants.CardErrorInvalidExpiry);
            }
        }

        private static void ValidateCardNumberOrThrow(Card card)
        {
            if (Regex.IsMatch(card.CardNumber, @"[a-zA-Z\W_]"))
            {
                throw new ArgumentException(CardConstants.CardErrorOnlyNumbers);
            }
        }
    }
}