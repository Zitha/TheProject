using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.Data
{
    public class ApplicationUnit : IDisposable
    {
        private readonly DataContext _context = new DataContext();
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
