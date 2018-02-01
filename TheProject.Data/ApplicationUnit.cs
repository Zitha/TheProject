using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheProject.Data.Repositories;
using TheProject.Model;

namespace TheProject.Data
{
    public class ApplicationUnit : IDisposable
    {
        private readonly DataContext _context = new DataContext();
        private IRepository<Facility> _facilities;

        public IRepository<Facility> Facilities
        {
            get
            {
                if (_facilities == null)
                {
                    _facilities = new FacilityRepository(_context);
                }
                return _facilities;
            }
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
