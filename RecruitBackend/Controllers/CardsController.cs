using System;
using System.Collections.Generic;
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

        [HttpGet("{cardNumber}")]
        public ActionResult<Card> GetByCardNumber(string cardNumber)
        {
            var card = _cardService.GetByCardNumber(cardNumber);
            if (card == null) return new NotFoundResult();
            return card;
        }

        [HttpGet]
        public IEnumerable<Card> Get()
        {
            return _cardService.GetAllCards();
        }

        [HttpPost]
        public ActionResult<Card> PostCard(Card card)
        {
            try
            {
                return _cardService.RegisterCard(card);
            }
            catch (ArgumentException e)
            {
                return new BadRequestResult();
            }
        }
    }
}