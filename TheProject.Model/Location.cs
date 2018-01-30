namespace TheProject.Model
{
    public class Location
    {
        public int Id { get; set; }

        public string StreetAdress { get; set; }

        public string Suburb { get; set; }

        public string Province { get; set; }

        public GPSCoordinate GPSCoordinates { get; set; }

    }
}