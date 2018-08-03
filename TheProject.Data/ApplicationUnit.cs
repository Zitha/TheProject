using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        private IRepository<Client> _clients;

        public IRepository<Client> Clients
        {
            get
            {
                if (_clients == null)
                {
                    _clients = new ClientRepository(_context);
                }
                return _clients;
            }
        }

        private IRepository<Person> _people;

        public IRepository<Person> People
        {
            get
            {
                if (_people == null)
                {
                    _people = new PersonRepository(_context);
                }
                return _people;
            }
        }

        private IRepository<DeedsInfo> _deedsInfos;

        public IRepository<DeedsInfo> DeedsInfos
        {
            get
            {
                if (_deedsInfos == null)
                {
                    _deedsInfos = new DeedsInfoRepository(_context);
                }
                return _deedsInfos;
            }
        }

        private IRepository<BoundryPolygon> _boundryPolygons;

        public IRepository<BoundryPolygon> BoundryPolygons
        {
            get
            {
                if (_deedsInfos == null)
                {
                    _boundryPolygons = new BoundryPolygonRepository(_context);
                }
                return _boundryPolygons;
            }
        }

        private IRepository<Location> _locations;

        public IRepository<Location> Locations
        {
            get
            {
                if (_locations == null)
                {
                    _locations = new LocationRepository(_context);
                }
                return _locations;
            }
        }

        private IRepository<GPSCoordinate> _gpsCoordinates;
        public IRepository<GPSCoordinate> GPSCoordinates
        {
            get
            {
                if (_gpsCoordinates == null)
                {
                    _gpsCoordinates = new GPSCoordinateRepository(_context);
                }
                return _gpsCoordinates;
            }
        }

        private IRepository<Audit> _audits;
        public IRepository<Audit> Audits
        {
            get
            {
                if (_audits == null)
                {
                    _audits = new AuditRepository(_context);
                }
                return _audits;
            }
        }


        private IRepository<ErrorLog> _errorLogs;
        public IRepository<ErrorLog> ErrorLogs
        {
            get
            {
                if (_errorLogs == null)
                {
                    _errorLogs = new ErrorLogRepository(_context);
                }
                return _errorLogs;
            }
        }

        private IRepository<OriginalData> _originalDatas;
        public IRepository<OriginalData> OriginalDatas
        {
            get
            {
                if (_originalDatas == null)
                {
                    _originalDatas = new OriginalDataRepository(_context);
                }
                return _originalDatas;
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

        //public DbRawSqlQuery<T> ExecuteStoredProc(string storeProName)
        //{
        //    var results = _context.ex

        //    return results;
        //}
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }


    }
}
