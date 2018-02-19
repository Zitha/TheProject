namespace TheProject.Model
{
    public class Location
    {
        public int Id { get; set; }

        public string StreetAddress { get; set; }

        public string Suburb { get; set; }

        public string Province { get; set; }

        public string LocalMunicipality { get; set; }

        public string Region { get; set; }
        public BoundryPolygon BoundryPolygon { get; set; }
        public GPSCoordinate GPSCoordinates { get; set; }
    }
}