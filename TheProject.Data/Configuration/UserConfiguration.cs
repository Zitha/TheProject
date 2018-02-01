using System.Data.Entity.ModelConfiguration;
using TheProject.Model;

namespace TheProject.Data.Configuration
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
        }
    }
}