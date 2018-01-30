using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.Model
{
    public class Facility
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ClientCode { get; set; }

        public string SettlementType { get; set; }

        public string Zoning { get; set; }

        public string MunicipalRoll { get; set; }

        public string IDPicture { get; set; }

        public GPSCoordinate GPSCoordinates { get; set; }

        public BoundryPolygon Polygon { get; set; }

        public DeedsInfo DeedsInfo { get; set; }

        public Person ResposiblePerson { get; set; }

        public Location Location { get; set; }

        public List<Building> Buildings { get; set; }
    }
}
