using System.Data.Entity;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class DeedsInfoRepository : GenericRepository<DeedsInfo>
    {
        public DeedsInfoRepository(DbContext context) : base(context)
        {
        }
    }
}
