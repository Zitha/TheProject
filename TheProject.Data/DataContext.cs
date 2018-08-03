using System.Configuration;
using System.Data.Entity;
using TheProject.Data.Configuration;
using TheProject.Model;

namespace TheProject.Data
{
    public class DataContext : DbContext
    {
        static DataContext()
        {
            Database.SetInitializer(new CustomDatabaseInitializer());
        }

        public DataContext()
            : base(ConnectionStringName)
        {
        }

        public static string ConnectionStringName
        {
            get
            {
                if (ConfigurationManager.AppSettings["ConnectionStringName"]
                    != null)
                {
                    return ConfigurationManager.
                        AppSettings["ConnectionStringName"];
                }

                return "DefaultConnection";
            }
        }

        public DbSet<BoundryPolygon> BoundryPoygons
        {
            get;
            set;
        }

        public DbSet<Building> Buildings
        {
            get;
            set;
        }

        public DbSet<Client> Clients
        {
            get;
            set;
        }

        public DbSet<DeedsInfo> DeedsInfos
        {
            get;
            set;
        }

        public DbSet<Facility> Facilities
        {
            get;
            set;
        }

        public DbSet<GPSCoordinate> GPSCoordinates
        {
            get;
            set;
        }

        public DbSet<Location> Locations
        {
            get;
            set;
        }

        public DbSet<Portfolio> Portfolios
        {
            get;
            set;
        }

        public DbSet<User> Users
        {
            get;
            set;
        }

        public DbSet<Audit> Audits
        {
            get;
            set;
        }

        public DbSet<ErrorLog> ErrorLogs
        {
            get;
            set;
        }

        public DbSet<OriginalData> OriginalDatas
        {
            get;
            set;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BoundryPolygonConfiguration());
            modelBuilder.Configurations.Add(new BuildingConfiguration());
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new DeedsInfoConfiguration());
            modelBuilder.Configurations.Add(new FacilityConfiguration());
            modelBuilder.Configurations.Add(new GPSCoordinateConfiguration());
            modelBuilder.Configurations.Add(new LocationConfiguration());
            modelBuilder.Configurations.Add(new PortfolioConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new AuditConfiguration());
            modelBuilder.Configurations.Add(new OriginalDataConfiguration());
        }

    }
}