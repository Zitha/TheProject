using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheProject.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using TheProject.Model;

namespace TheProject.Api.Controllers.Tests
{
    [TestClass()]
    public class LocationControllerTests
    {      
        [TestMethod()]
        public void CreateTest()
        {
            var controller = new LocationController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var boundryPolygon = new List<BoundryPolygon>();
            Location location = new Location()
            {
                Id = 0,
                StreetAddress = "Street a",
                LocalMunicipality = "",
                Region = "JHB",
                Province = "Gauteng",
                GPSCoordinates = new GPSCoordinate() {
                    Latitude = "-26.3258",
                    Longitude = "28.3658"
                },                
                //CreatedDate = DateTime.Now,
                //CreatedUserId = 1,
                //FacilityId = 5
            };
            boundryPolygon.Add(new BoundryPolygon() { Longitude = "24.3698",Latitude="-25.3652"});
            boundryPolygon.Add(new BoundryPolygon() { Longitude = "26.3698", Latitude = "-26.3652" });
            location.BoundryPolygon = boundryPolygon;
            var result = controller.CreateEdit(location);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        [TestMethod()]
        public void EditTest()
        {
            var controller = new LocationController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var boundryPolygon = new List<BoundryPolygon>();
            Location location = new Location()
            {
                Id = 10,
                StreetAddress = "Street Address 110 4545445454",
                LocalMunicipality = "test",
                Region = "JHB",
                Suburb = "Test",
                Province = "Gauteng",
                GPSCoordinates = new GPSCoordinate()
                {
                    Id = 24,
                    Latitude = "-26.325822222",
                    Longitude = "28.3658 2222"
                },
                //CreatedDate = DateTime.Now,
                //CreatedUserId = 1,
                //FacilityId = 5
            };
            boundryPolygon.Add(new BoundryPolygon() { Longitude = "25.3698", Latitude = "-25.3652" });
            boundryPolygon.Add(new BoundryPolygon() { Longitude = "11.3698", Latitude = "-11.3652" });
            boundryPolygon.Add(new BoundryPolygon() { Longitude = "2225.3698", Latitude = "-2225.3652" });
            location.BoundryPolygon = boundryPolygon;
            var result = controller.CreateEdit(location);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        [TestMethod()]
        public void GetLocationByIdTest()
        {
            var controller = new LocationController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.GetLocationById(3);
            Assert.IsNotNull(result.Id);
        }
    }
}