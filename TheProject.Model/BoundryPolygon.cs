using System.Collections.Generic;

namespace TheProject.Model
{
    public class BoundryPolygon
    {
        public int Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public virtual Location Location { get; set; }
    }
}