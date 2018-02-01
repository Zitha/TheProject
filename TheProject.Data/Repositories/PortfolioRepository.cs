using System.Data.Entity;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class PortfolioRepository : GenericRepository<Portfolio>
    {
        public PortfolioRepository(DbContext context) : base(context)
        {
        }
    }
}
