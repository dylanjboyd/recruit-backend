using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitBackend.Models;
using RecruitBackend.Services;

namespace RecruitBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly ILogger<CardsController> _logger;

        public CardsController(ILogger<CardsController> logger, ICardService cardService)
        {
            _logger = logger;
            _cardService = cardService;
        }

        /// <summary>
        /// Fetches the first matching card by card number.
        /// </summary>
        /// <param name="cardNumber">The number associated with the card to fetch.</param>
        /// <returns>The first matching card.</returns>
        [HttpGet("{cardNumber}", Name = "Fetch card by card number")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Card> GetByCardNumber(string cardNumber)
        {
            var card = _cardService.GetByCardNumber(cardNumber);
            if (card == null) return new NotFoundResult();
            return card;
        }

        /// <summary>
        /// Fetches all stored cards.
        /// </summary>
        /// <returns>All cards on record.</returns>
        [HttpGet(Name = "Fetch all stored cards")]
        public IEnumerable<Card> Get()
        {
            return _cardService.GetAllCards();
        }

        /// <summary>
        /// Registers a new card.
        /// </summary>
        /// <param name="card">The card to be registered</param>
        /// <returns>The created card with any updated values</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Card> PostCard(Card card)
        {
            try
            {
                return _cardService.RegisterCard(card);
            }
            catch (ArgumentException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}