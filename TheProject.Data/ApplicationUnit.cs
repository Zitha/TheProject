using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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


        private IRepository<Building> _buildings;

        public IRepository<Building> Buildings
        {
            get
            {
                if (_buildings == null)
                {
                    _buildings = new BuildingRepository(_context);
                }
                return _buildings;
            }
        }

        private IRepository<Portfolio> _portfolios;

        public IRepository<Portfolio> Portfolios
        {
            get
            {
                if (_portfolios == null)
                {
                    _portfolios = new PortfolioRepository(_context);
                }
                return _portfolios;
            }
        }

        private IRepository<User> _users;

        public IRepository<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_context);
                }
                return _users;
            }
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
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
