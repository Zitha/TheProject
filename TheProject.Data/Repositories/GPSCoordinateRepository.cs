using System.Data.Entity;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class GPSCoordinateRepository : GenericRepository<GPSCoordinate>
    {
        public GPSCoordinateRepository(DbContext context) : base(context)
        {
        }
    }
}
