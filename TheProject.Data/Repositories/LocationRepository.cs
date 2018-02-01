using System.Data.Entity;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class LocationRepository : GenericRepository<Location>
    {
        public LocationRepository(DbContext context) : base(context)
        {
        }
    }
}
