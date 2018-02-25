using System.Data.Entity;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class AuditRepository : GenericRepository<Audit>
    {
        public AuditRepository(DbContext context) : base(context)
        {
        }
    }
}
