using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TheProject.Data;
using TheProject.Model;

namespace TheProject.Api.Controllers
{
    public class BuildingController : ApiController
    {

        string _picturePath = HttpContext.Current.Server.MapPath("~/UploadPictures");
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
                            building.BuildingName = building.BuildingName.Trim();
                            building.BuildingNumber = building.BuildingNumber.Trim();
                            building.BuildingType = building.BuildingType.Trim();
                            building.BuildingStandard = building.BuildingStandard.Trim();
                            building.Status = building.Status.Trim();
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

        [HttpPost]
        public Building UpdateBuilding(Building building)
        {
            try
            {
                ApplicationUnit unit = new ApplicationUnit();
                Building updateBudilng = unit.Buildings.GetAll().FirstOrDefault(fc => fc.Id == building.Id);
                if (updateBudilng != null)
                {
                    updateBudilng.BuildingName = building.BuildingName.Trim();
                    updateBudilng.BuildingNumber = building.BuildingNumber.Trim();
                    updateBudilng.BuildingType = building.BuildingType.Trim();
                    updateBudilng.BuildingStandard = building.BuildingStandard.Trim();
                    updateBudilng.Status = building.Status.Trim();
                    updateBudilng.GPSCoordinates = GPSCoordinates(building.GPSCoordinates, ref unit);
                    updateBudilng.NumberOfFloors = building.NumberOfFloors;
                    updateBudilng.FootPrintArea = building.FootPrintArea;
                    updateBudilng.ImprovedArea = building.ImprovedArea;
                    updateBudilng.Heritage = building.Heritage;
                    updateBudilng.OccupationYear = building.OccupationYear;
                    updateBudilng.DisabledAccess = building.DisabledAccess;
                    updateBudilng.DisabledComment = building.DisabledComment;
                    updateBudilng.ConstructionDescription = building.ConstructionDescription;
                    updateBudilng.Photo = building.Photo;
                    updateBudilng.ModifiedDate = DateTime.Now;

                    unit.Buildings.Update(updateBudilng);
                    unit.SaveChanges();

                    building.Facility = null;
                    return building;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private GPSCoordinate GPSCoordinates(GPSCoordinate gpsCoordinate, ref ApplicationUnit unit)
        {
            if (gpsCoordinate != null)
            {
                GPSCoordinate gps = unit.GPSCoordinates.GetAll()
                    .FirstOrDefault(p => p.Id == gpsCoordinate.Id);
                if (gps != null)
                {
                    return gps;
                }
            }
            return gpsCoordinate;
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
                            Id = building.Id,
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
                            Id = building.Id,
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
        [HttpPost]
        public void SaveImage([FromBody]List<Picture> pictures)
        {
            if (!Directory.Exists(_picturePath))
            {
                Directory.CreateDirectory(_picturePath);
            }
            foreach (var picture in pictures)
            {
                using (FileStream fileStream = new FileStream(Path.Combine(_picturePath, picture.Name + ".png"), FileMode.Create))
                {
                    fileStream.Write(picture.File, 0, picture.File.Length);
                    fileStream.Close();
                }
            }
        }
    }
}
