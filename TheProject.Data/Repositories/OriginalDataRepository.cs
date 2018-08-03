using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class OriginalDataRepository : GenericRepository<OriginalData>
    {
        public OriginalDataRepository(DbContext context) : base(context)
        {

        }
    }
}
