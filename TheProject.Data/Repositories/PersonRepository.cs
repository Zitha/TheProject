using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class PersonRepository : GenericRepository<Person>
    {
        public PersonRepository(DbContext context) : base(context)
        {
        }
    }
}
