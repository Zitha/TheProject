using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public Building AddBuilding(Building building)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    Facility facility = unit.Facilities.GetAll().Include(b => b.Buildings)
                        .FirstOrDefault(fc => fc.Id == building.Facility.Id);

                    if (facility != null)
                    {
                        bool hasBuilding = facility.Buildings.Exists(b => b.BuildingName == building.BuildingName);
                        if (!hasBuilding)
                        {
                            building.Facility = facility;
                            building.CreatedDate = DateTime.Now;
                            building.ModifiedDate = DateTime.Now;
                            unit.Buildings.Add(building);

                            unit.SaveChanges();
                            building.Facility = null;
                            return building;
                        }
                    }
                    return null;
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

                    building.Facility = null;
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
                List<Building> returnBuildings = new List<Building>();
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    List<Building> buildings = unit.Facilities.GetAll()
                        .Where(fa => fa.Id == facilityId)
                        .Select(fc => fc.Buildings).FirstOrDefault();
                    foreach (var building in buildings)
                    {
                        returnBuildings.Add(new Building
                        {

                            BuildingName = building.BuildingName,
                            BuildingNumber = building.BuildingNumber,
                            BuildingStandard = building.BuildingStandard,
                            Status = building.Status,
                            BuildingType = building.BuildingType,
                            NumberOfFloors = building.NumberOfFloors,
                            FootPrintArea = building.FootPrintArea,
                            ImprovedArea = building.ImprovedArea,
                            Heritage = building.Heritage,
                            OccupationYear = building.OccupationYear,
                            DisabledAccess = building.DisabledAccess,
                            DisabledComment = building.DisabledComment,
                            ConstructionDescription = building.ConstructionDescription,
                            GPSCoordinates = building.GPSCoordinates,
                            Photo = building.Photo,
                            CreatedDate = building.CreatedDate,
                            ModifiedDate = building.ModifiedDate

                        });
                    }

                    return returnBuildings;
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


        [HttpGet]
        public IEnumerable<Building> GetBuildings()
        {
            try
            {
                List<Building> returnBuildings = new List<Building>();
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    List<Building> buildings = unit.Buildings.GetAll().ToList();
                    foreach (var building in buildings)
                    {
                        returnBuildings.Add(new Building
                        {

                            BuildingName = building.BuildingName,
                            BuildingNumber = building.BuildingNumber,
                            BuildingStandard = building.BuildingStandard,
                            Status = building.Status,
                            BuildingType = building.BuildingType,
                            NumberOfFloors = building.NumberOfFloors,
                            FootPrintArea = building.FootPrintArea,
                            ImprovedArea = building.ImprovedArea,
                            Heritage = building.Heritage,
                            OccupationYear = building.OccupationYear,
                            DisabledAccess = building.DisabledAccess,
                            DisabledComment = building.DisabledComment,
                            ConstructionDescription = building.ConstructionDescription,
                            GPSCoordinates = building.GPSCoordinates,
                            Photo = building.Photo,
                            CreatedDate = building.CreatedDate,
                            ModifiedDate = building.ModifiedDate

                        });
                    }

                    return returnBuildings;
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
