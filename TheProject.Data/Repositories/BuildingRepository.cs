using System.Data.Entity;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class BuildingRepository : GenericRepository<Building>
    {
        public BuildingRepository(DbContext context) : base(context)
        {
        }
    }
}
