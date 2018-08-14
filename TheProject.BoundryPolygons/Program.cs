using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.BoundryPolygons
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadExcelData();
        }

        private static void ReadExcelData()
        {
            string _currpath = ConfigurationManager.AppSettings["PolygonsPath"];
            TheProjectEntities db = new TheProjectEntities();
            StreamReader sr = new StreamReader(_currpath);
            string line;
            string[] row = new string[5];
            int rowsNumber = 0;
            
            while ((line = sr.ReadLine()) != null)
            {
                int latitudeCount = 1;
                int longitudeCount = 2;
                row = line.Split(',');
                List<string> data = row[0].Split(';').ToList<string>();

                if (rowsNumber != 0)
                {
                    string clientCode = data[0];
                    var facility = db.Facilities.FirstOrDefault(ss => ss.ClientCode.Trim().ToLower() == clientCode.Trim().ToLower());

                    if (facility != null) {
                        foreach (var item in data)
                        {
                            try
                            {
                                if (string.IsNullOrEmpty(data[latitudeCount]))
                                {
                                    break;
                                }
                                else
                                {
                                    string latitude = data[latitudeCount];
                                    string longitude = data[longitudeCount];
                                    latitudeCount = latitudeCount + 2;
                                    longitudeCount = longitudeCount + 2;

                                    BoundryPolygon boundryPolygon = new BoundryPolygon()
                                    {
                                        Longitude = longitude,
                                        Latitude = latitude,
                                        Location_Id = facility.Location_Id
                                    };
                                    db.BoundryPolygons.Add(boundryPolygon);
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception)
                            {
                                break;
                            }
                           
                        }
                    }                    
                }                
                rowsNumber++;
            }
        }
    }
}
