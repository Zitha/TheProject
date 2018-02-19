using Newtonsoft.Json;
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
    public class FacilityController : ApiController
    {
        [HttpPut]
        public bool UpdateFacility(Facility facility)
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
                    updateFacility.DeedsInfo = GetDeedsInfo(facility.DeedsInfo, ref unit);
                    updateFacility.ResposiblePerson = GetReposiblePerson(facility.ResposiblePerson, ref unit);
                    updateFacility.Location = GetLocation(facility.Location, ref unit);
                    updateFacility.Buildings = facility.Buildings;
                    updateFacility.Status = facility.Status;

                    unit.Facilities.Update(updateFacility);
                    unit.SaveChanges();
                    unit.Dispose();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                unit.Dispose();
                throw ex;
            }
        }

        private Location GetLocation(Location location, ref ApplicationUnit unit)
        {
            Location loc = unit.Locations.GetAll().First(p => p.Id == location.Id);
            if (loc != null)
            {
                return loc;
            }
            return location;
        }

        private Person GetReposiblePerson(Person resposiblePerson, ref ApplicationUnit unit)
        {
            Person person = unit.People.GetAll().First(p => p.EmailAddress == resposiblePerson.EmailAddress);
            if (person != null)
            {
                return person;
            }
            return resposiblePerson;
        }

        private DeedsInfo GetDeedsInfo(DeedsInfo deedsInfo, ref ApplicationUnit unit)
        {
            DeedsInfo deeds = unit.DeedsInfos.GetAll().First(p => p.Id == deedsInfo.Id);
            if (deeds != null)
            {
                return deeds;
            }
            return deedsInfo;
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
                var outputLines = new List<string>
                {
                    ex.Message
                };
                File.AppendAllLines(@"c:\errors.txt", outputLines);
                throw;
            }
        }

        private List<Building> GetBuildings(List<Building> buildings)
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

        [HttpGet]
        public IEnumerable<Facility> GetFacilitiesByUserId(int userId)
        {
            try
            {
                List<Facility> returnFacilities = new List<Facility>();
                using (ApplicationUnit unit = new ApplicationUnit())
                {
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
                                Location = facility.Location,
                                CreatedDate = facility.CreatedDate,
                                ModifiedDate = facility.ModifiedDate
                            });
                        }
                    }
                    return returnFacilities;
                }
            }
            catch (Exception ex)
            {
                var outputLines = new List<string>
                {
                    ex.Message
                };
                File.AppendAllLines(@"c:\errors.txt", outputLines);
                throw;
            }
        }
    }
}
