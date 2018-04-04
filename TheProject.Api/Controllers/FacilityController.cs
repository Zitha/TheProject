using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TheProject.Api.Models;
using TheProject.Data;
using TheProject.Model;

namespace TheProject.Api.Controllers
{
    public class FacilityController : ApiController
    {
        [HttpPut]
        public HttpResponseMessage UpdateFacility(Facility facility)
        {
            ApplicationUnit unit = new ApplicationUnit();

            Facility updateFacility = unit.Facilities.GetAll().FirstOrDefault(fc => fc.Id == facility.Id);

            try
            {
                if (updateFacility != null)
                {
                    updateFacility.SettlementType = facility.SettlementType;
                    updateFacility.Zoning = facility.Zoning;
                    updateFacility.Name = facility.Name;
                    updateFacility.IDPicture = facility.IDPicture;
                    updateFacility.Status = facility.Status;
                    updateFacility.ModifiedUserId = facility.ModifiedUserId;
                    updateFacility.ModifiedDate = DateTime.Now;

                    unit.Facilities.Update(updateFacility);
                    unit.SaveChanges();
                    unit.Dispose();

                    int userId = updateFacility.ModifiedUserId != null ? facility.ModifiedUserId.Value : 0;
                    Task updateTask = new Task(() => LogAuditTrail("Facility", "Update", userId, updateFacility.Id));
                    updateTask.Start();

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        content = true
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    content = true
                });
            }
            catch (Exception ex)
            {
                unit.Dispose();
                ErrorHandling.LogError(ex.StackTrace, "UpdateFacility");
                return Request.CreateResponse(ex);
            }
        }

        private Location GetLocation(Location location, ref ApplicationUnit unit)
        {
            if (location != null)
            {
                Location loc = unit.Locations.GetAll()
                    .Include(gps => gps.GPSCoordinates)
                    .Include(bp => bp.BoundryPolygon)
                    .FirstOrDefault(p => p.Id == location.Id);

                if (loc != null)
                {
                    if (location.BoundryPolygon != null)
                    {
                        foreach (var newPolygon in location.BoundryPolygon)
                        {
                            BoundryPolygon polygon = loc.BoundryPolygon.FirstOrDefault(d => d.Id == newPolygon.Id);
                            if (polygon != null)
                            {
                                polygon.Latitude = newPolygon.Latitude;
                                polygon.Longitude = newPolygon.Longitude;
                            }
                        }
                    }
                    foreach (var polygon in loc.BoundryPolygon)
                    {
                        polygon.Location = null;
                    }
                    return loc;
                }
            }

            return location;
        }

        private Person GetReposiblePerson(Person resposiblePerson, ref ApplicationUnit unit)
        {
            if (resposiblePerson != null)
            {
                Person person = unit.People.GetAll().FirstOrDefault(p => p.EmailAddress == resposiblePerson.EmailAddress);
                if (person != null)
                {
                    return person;
                }
            }

            return resposiblePerson;
        }

        private DeedsInfo GetDeedsInfo(DeedsInfo deedsInfo, ref ApplicationUnit unit)
        {
            if (deedsInfo != null)
            {
                DeedsInfo deeds = unit.DeedsInfos.GetAll().FirstOrDefault(p => p.Id == deedsInfo.Id);
                if (deeds != null)
                {
                    return deeds;
                }
            }
            return deedsInfo;
        }

        [HttpGet]
        public List<Facility> GetUnassignedFacilities()
        {
            try
            {
                List<Facility> returnFacilities = new List<Facility>();
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    List<Facility> facilities = unit.Facilities.ExecuteStoredProc("GetUnassignedFacilities").ToList();
                    foreach (var facility in facilities)
                    {
                        returnFacilities.Add(new Facility
                        {
                            Id = facility.Id,
                            Name = facility.Name,
                            ClientCode = facility.ClientCode,
                            SettlementType = facility.SettlementType,
                            Zoning = facility.Zoning,
                            IDPicture = facility.IDPicture,
                            Status = facility.Status,
                            DeedsInfo = facility.DeedsInfo,
                            ResposiblePerson = facility.ResposiblePerson,
                            Location = facility.Location,
                            CreatedDate = facility.CreatedDate,
                            ModifiedDate = facility.ModifiedDate
                        });
                    }
                    return returnFacilities;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace, "GetUnassignedFacilities");
                throw ex;
            }
        }

        [HttpPost]
        public Facility AddFacility(Facility facility)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    Portfolio portfolio = unit.Portfolios.GetAll()
                        .FirstOrDefault(p => p.Id == facility.Portfolio.Id);

                    Facility hasFacility = unit.Facilities.GetAll()
                     .FirstOrDefault(u => u.Name.ToLower() == facility.Name.ToLower());


                    if (hasFacility == null)
                    {
                        facility.CreatedDate = DateTime.Now;
                        facility.ModifiedDate = DateTime.Now;
                        facility.Portfolio = portfolio;
                        facility.Status = "New";
                        unit.Facilities.Add(facility);
                        unit.SaveChanges();
                        return facility;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace, "AddFacility");
                throw ex;
            }
        }

        [HttpGet]
        public List<Facility> GetFacilities()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();
            try
            {
                List<Facility> returnFacilities = new List<Facility>();
                using (ApplicationUnit unit = new ApplicationUnit())
                {

                    var facilitiesQuery = unit.Facilities.GetAll().ToList();
                    foreach (var facility in facilitiesQuery)
                    {
                        returnFacilities.Add(new Facility
                        {
                            Id = facility.Id,
                            Name = facility.Name,
                            ClientCode = facility.ClientCode,
                            SettlementType = facility.SettlementType,
                            Zoning = facility.Zoning,
                            IDPicture = facility.IDPicture,
                            Status = facility.Status,
                            DeedsInfo = facility.DeedsInfo,
                            ResposiblePerson = facility.ResposiblePerson,
                            Location = facility.Location,
                            CreatedDate = facility.CreatedDate,
                            ModifiedDate = facility.ModifiedDate,
                            Buildings = GetBuildings(facility.Buildings)
                        });
                    }
                    return returnFacilities;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace, "GetFacilities");
                throw ex;
            }
        }

        private List<Building> GetBuildings(List<Building> buildings)
        {
            try
            {
                List<Building> returnBuildings = new List<Building>();
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
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace, "GetBuildings");
                throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<Facility> GetFacilitiesByUserId(int userId)
        {
            try
            {
                List<Facility> returnFacilities = new List<Facility>();
                ApplicationUnit unit = new ApplicationUnit();

                List<Facility> facilities = unit.Users.GetAll()
                    .Where(usr => usr.Id == userId)
                    .Select(fc => fc.Facilities).FirstOrDefault();

                foreach (var facility in facilities)
                {
                    if (facility.Status == "New")
                    {
                        returnFacilities.Add(new Facility
                        {
                            Id = facility.Id,
                            Name = facility.Name,
                            ClientCode = facility.ClientCode,
                            SettlementType = facility.SettlementType,
                            Zoning = facility.Zoning,
                            IDPicture = facility.IDPicture,
                            Status = facility.Status,
                            DeedsInfo = facility.DeedsInfo,
                            ResposiblePerson = facility.ResposiblePerson,
                            Location = GetLocation(facility.Location, ref unit),
                            CreatedDate = facility.CreatedDate,
                            ModifiedDate = facility.ModifiedDate
                        });
                    }
                }
                return returnFacilities;
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace, "GetFacilitiesByUserId");
                throw;
            }
        }


        private void LogAuditTrail(string section, string type, int userId, int itemId)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    Audit audit = new Audit
                    {
                        ChangeDate = DateTime.Now,
                        ItemId = itemId,
                        Section = section,
                        UserId = userId,
                        Type = type
                    };
                    unit.Audits.Add(audit);
                    unit.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
