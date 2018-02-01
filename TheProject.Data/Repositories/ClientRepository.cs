using System.Data.Entity;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class ClientRepository : GenericRepository<Client>
    {
        public ClientRepository(DbContext context) : base(context)
        {
        }
    }
}
