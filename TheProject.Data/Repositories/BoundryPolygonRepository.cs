using System.Data.Entity;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class BoundryPolygonRepository : GenericRepository<BoundryPolygon>
    {
        public BoundryPolygonRepository(DbContext context) : base(context)
        {
        }
    }
}
