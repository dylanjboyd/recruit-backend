using RecruitBackend.Models;

namespace RecruitBackend.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
    }

    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(DatabaseContext context) : base(context)
        {
        }
    }
}