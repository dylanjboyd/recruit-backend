using System.Linq;
using RecruitBackend.Models;

namespace RecruitBackend.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
        Card GetByCardNumber(string cardNumber);
    }

    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(DatabaseContext context) : base(context)
        {
        }

        public Card GetByCardNumber(string cardNumber)
        {
            return Context.Cards.SingleOrDefault(c => c.CardNumber == cardNumber);
        }
    }
}