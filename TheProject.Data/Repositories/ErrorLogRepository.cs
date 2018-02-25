using System;
using System.Data.Entity;
using TheProject.Model;

namespace TheProject.Data.Repositories
{
    public class ErrorLogRepository : GenericRepository<ErrorLog>
    {
        public ErrorLogRepository(DbContext context) : base(context)
        {

        }
    }
}
