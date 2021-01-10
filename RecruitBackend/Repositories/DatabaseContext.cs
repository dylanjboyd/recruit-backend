using Microsoft.EntityFrameworkCore;
using RecruitBackend.Models;

namespace RecruitBackend.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<ValidCard> ValidCards { get; set; }
    }
}