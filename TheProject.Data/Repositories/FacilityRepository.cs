using System.Data.Entity;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class FacilityRepository : GenericRepository<Facility>
    {
        public FacilityRepository(DbContext context) : base(context)
        {
        }
    }
}
