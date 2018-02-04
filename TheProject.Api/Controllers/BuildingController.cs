using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheProject.Data;
using TheProject.Model;

namespace TheProject.Api.Controllers
{
    public class BuildingController : ApiController
    {
        [HttpPost]
        public Building AddBulding(Building building)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    unit.Buildings.Add(building);
                    unit.SaveChanges();
                    return building;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        public Building UpdateFacility(Building building)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    Building updateBudilng = unit.Buildings.GetAll().FirstOrDefault(fc => fc.Id == building.Id);
                    if (updateBudilng != null)
                    {
                        updateBudilng.BuildingName = building.BuildingName;
                        updateBudilng.BuildingNumber = building.BuildingNumber;
                        updateBudilng.BuildingType = building.BuildingType;
                        updateBudilng.BuildingStandard = building.BuildingStandard;
                        updateBudilng.Status = building.Status;
                        updateBudilng.GPSCoordinates = building.GPSCoordinates;
                        updateBudilng.NumberOfFloors = building.NumberOfFloors;
                        updateBudilng.FootPrintArea = building.FootPrintArea;
                        updateBudilng.ImprovedArea = building.ImprovedArea;
                        updateBudilng.Heritage = building.Heritage;
                        updateBudilng.OccupationYear = building.OccupationYear;
                        updateBudilng.DisabledAccess = building.DisabledAccess;
                        updateBudilng.DisabledComment = building.DisabledComment;
                        updateBudilng.ConstructionDescription = building.ConstructionDescription;
                        updateBudilng.Photo = building.Photo;
                    }

                    unit.Buildings.Add(building);
                    unit.SaveChanges();
                    return building;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<Building> GetBuildingByFacilityId(int facilityId)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    List<Building> buildings = unit.Facilities.GetAll()
                        .Where(fa => fa.Id == facilityId)
                        .Select(fc => fc.Buildings).FirstOrDefault();

                    return buildings;
                }
            }
            catch (Exception ex)
            {
                var outputLines = new List<string>();
                outputLines.Add(ex.Message);
                File.AppendAllLines(@"c:\errors.txt", outputLines);
                throw;
            }
        }
    }
}
