using System.Collections.Generic;

namespace TheProject.Model
{
    public class BoundryPolygon
    {
        public int Id { get; set; }
        public List<GPSCoordinate> GPSCoordinates { get; set; }
    }
}