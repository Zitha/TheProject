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
            using (ApplicationUnit unit = new ApplicationUnit())
            {
                Facility updateFacility = unit.Facilities.GetAll().FirstOrDefault(fc => fc.Id == facility.Id);

                try
                {
                    if (updateFacility != null)
                    {
                        updateFacility.SettlementType = facility.SettlementType;
                        updateFacility.Zoning = facility.Zoning;
                        updateFacility.MunicipalRoll = facility.MunicipalRoll;
                        updateFacility.Name = facility.Name;
                        updateFacility.IDPicture = facility.IDPicture;
                        updateFacility.GPSCoordinates = facility.GPSCoordinates;
                        updateFacility.Polygon = facility.Polygon;
                        updateFacility.DeedsInfo = facility.DeedsInfo;
                        updateFacility.ResposiblePerson = facility.ResposiblePerson;
                        updateFacility.Location = facility.Location;
                        updateFacility.Buildings = facility.Buildings;

                        unit.Facilities.Update(updateFacility);
                        unit.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        [HttpPost]
        public Facility AddFacility(Facility facility)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    Facility hasFacility = unit.Facilities.GetAll()
                        .FirstOrDefault(u => u.Name.ToLower() == facility.Name.ToLower());

                    if (hasFacility == null)
                    {
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
        public IEnumerable<Facility> GetFacilities()
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    List<Facility> facilities = unit.Facilities.GetAll()
                        .Include(gps => gps.GPSCoordinates)
                        .Include(pol => pol.Polygon)
                        .Include(deds => deds.DeedsInfo)
                        .Include(rp => rp.ResposiblePerson)
                        .Include(lc => lc.Location)
                        .Include(bd => bd.Buildings)
                        .ToList();

                    return facilities;
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
        public IEnumerable<Facility> GetFacilitiesByUserId(int userId)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {

                    List<Facility> facilities = unit.Users.GetAll()
                        .Where(usr => usr.Id == userId)
                        .Select(fc => fc.Facilities).FirstOrDefault();

                    return facilities;
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
