using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class LocationController : ApiController
    {
        // GET: api/Location
        [HttpGet]
        public Location GetLocationById(int id)
        {
            try
            {
                ApplicationUnit unit = new ApplicationUnit();
                return unit.Locations.GetById(id);
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace, "GetLocation");
                throw ex;
            }
        }

        // POST: Location/CreateEdit
        [HttpPost]
        public HttpResponseMessage CreateEdit(Location location)
        {
            ApplicationUnit unit = new ApplicationUnit();
            string action = location.Id != 0 ? "Update" : "Create";
            int userId = 0;
            try
            {
                if (location != null)
                {                    

                    var BPs = unit.BoundryPolygons.GetAll().Where(d => d.Location.Id == location.Id).ToList();
                    foreach (var item in BPs)
                    {
                        unit.BoundryPolygons.Delete(item);
                    }
                    if (location.GPSCoordinates != null)
                    {
                        if (location.GPSCoordinates.Id != 0)
                        {
                            var GPS = unit.GPSCoordinates.GetById(location.GPSCoordinates.Id);
                            if (GPS != null)
                            {
                                unit.GPSCoordinates.Delete(GPS);
                            }                            
                        }
                    }

                    if (location.Id == 0) {
                      //  userId = location.CreatedUserId;
                        Create(ref unit, location);
                    }                       
                    else {
                        Location _location = unit.Locations.GetAll().Include(gps => gps.GPSCoordinates).Include(bp => bp.BoundryPolygon).FirstOrDefault(p => p.Id == location.Id);
                        _location.StreetAddress = location.StreetAddress;
                        _location.Region = location.Region;
                        _location.Suburb = location.Suburb;
                        _location.LocalMunicipality = location.LocalMunicipality;
                        _location.Province = location.Province;
                       // _location.FacilityId = location.FacilityId;
                        _location.GPSCoordinates = location.GPSCoordinates;
                        _location.BoundryPolygon = location.BoundryPolygon;
                        Update(ref unit, _location);
                    }
                }

                unit.Dispose();
                Task updateTask = new Task(() => LogAuditTrail("Location", action, userId, location.Id));
                updateTask.Start();
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    content = true
                });
            }
            catch (Exception ex)
            {
                unit.Dispose();
                ErrorHandling.LogError(ex.StackTrace, action + "Location");
                return Request.CreateResponse(ex);
            }
        }

        private void Create(ref ApplicationUnit unit, Location location)
        {
           // location.CreatedDate = DateTime.Now;

            //Facility updateFacility = unit.Facilities.GetAll().FirstOrDefault(fc => fc.Id == location.FacilityId);
            //if (updateFacility != null)
            //{
            //    updateFacility.Location = location;
            //    unit.Facilities.Update(updateFacility);
            //    unit.SaveChanges();
            //}
        }

        private void Update(ref ApplicationUnit unit, Location location)
        {
            //Facility updateFacility = unit.Facilities.GetAll().FirstOrDefault(fc => fc.Id == location.FacilityId);
            //if (updateFacility != null)
            //{
            //    updateFacility.Location.StreetAddress = location.StreetAddress;
            //    updateFacility.Location.Suburb = location.Suburb;
            //    updateFacility.Location.Province = location.Province;
            //    updateFacility.Location.LocalMunicipality = location.LocalMunicipality;
            //    updateFacility.Location.Region = location.Region;
            //    updateFacility.Location.ModifiedUserId = location.ModifiedUserId;
            //    updateFacility.Location.ModifiedDate = DateTime.Now;
            //    unit.Facilities.Update(updateFacility);
            //    unit.SaveChanges();
            //}
        }
        private void CreateEditBoundryPolygon(List<BoundryPolygon> boundryPolygons, ref ApplicationUnit unit, Location location)
        {          
            foreach (var boundryPolygon in boundryPolygons)
            {
                boundryPolygon.Location = location;
                location.BoundryPolygon.Add(boundryPolygon);                
            }
            unit.SaveChanges();
        }

        private void CreateEditGPSCoordinates(GPSCoordinate GPSCoordinates, ref ApplicationUnit unit)
        {
            if (GPSCoordinates.Id != 0)
                unit.GPSCoordinates.Update(GPSCoordinates);
            else
                unit.GPSCoordinates.Add(GPSCoordinates);
            unit.SaveChanges();
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
