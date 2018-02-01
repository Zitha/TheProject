using System.Data.Entity;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
