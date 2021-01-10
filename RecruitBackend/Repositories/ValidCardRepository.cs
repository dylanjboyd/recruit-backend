using System.Linq;
using RecruitBackend.Models;

namespace RecruitBackend.Repositories
{
    public interface IValidCardRepository
    {
        ValidCard GetByCardNumber(string cardNumber);
    }

    public class ValidCardRepository : Repository<ValidCard>, IValidCardRepository
    {
        protected ValidCardRepository(DatabaseContext context) : base(context)
        {
        }

        public ValidCard GetByCardNumber(string cardNumber)
        {
            return Context.ValidCards.SingleOrDefault(s => s.CardNumber == cardNumber);
        }
    }
}