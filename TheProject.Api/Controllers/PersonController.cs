using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TheProject.Api.Models;
using TheProject.Data;
using TheProject.Model;

namespace TheProject.Api.Controllers
{
    public class PersonController : ApiController
    {
        // GET: Person/Details/5
        [HttpGet]
        public Person GetPersonById(int id)
        {
            try
            {
                ApplicationUnit unit = new ApplicationUnit();
                return unit.People.GetById(id);
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace, "GetBuildings");
                throw ex;
            }
        }

        // POST: Person/CreateEdit
        [HttpPost]
        public HttpResponseMessage CreateEdit(Person resposiblePerson)
        {
            ApplicationUnit unit = new ApplicationUnit();
            string action = resposiblePerson.Id != 0 ? "Update" : "Create";
            int userId = 0;
            try
            {
                if (resposiblePerson != null)
                {
                    if (resposiblePerson.Id == 0)
                    {
                   //     userId = resposiblePerson.CreatedUserId;
                        Create(ref unit, resposiblePerson);
                    }
                    else
                    {
                    //    userId = resposiblePerson.ModifiedUserId.Value;
                        Update(ref unit, resposiblePerson);
                    }
                }
                 unit.Dispose();
                
                Task updateTask = new Task(() => LogAuditTrail("Person", action, userId, resposiblePerson.Id));
                updateTask.Start();
               
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    content = true
                });
            }
            catch (Exception ex)
            {
                unit.Dispose();
                ErrorHandling.LogError(ex.StackTrace, action + "Person");
                return Request.CreateResponse(ex);
            }
        }

        private void Create(ref ApplicationUnit unit, Person resposiblePerson)
        {
            //resposiblePerson.CreatedDate = DateTime.Now;
            //Facility updateFacility = unit.Facilities.GetAll().FirstOrDefault(fc => fc.Id == resposiblePerson.FacilityId);
            //if (updateFacility != null)
            //{
            //    updateFacility.ResposiblePerson = resposiblePerson;
            //    unit.Facilities.Update(updateFacility);
            //    unit.SaveChanges();
            //}
        }

        private void Update(ref ApplicationUnit unit, Person resposiblePerson)
        {
            //Facility updateFacility = unit.Facilities.GetAll().FirstOrDefault(fc => fc.Id == resposiblePerson.FacilityId);
            //if (updateFacility != null)
            //{
            //    updateFacility.ResposiblePerson.FullName = resposiblePerson.FullName;
            //    updateFacility.ResposiblePerson.Designation = resposiblePerson.Designation;
            //    updateFacility.ResposiblePerson.PhoneNumber = resposiblePerson.PhoneNumber;
            //    updateFacility.ResposiblePerson.EmailAddress = resposiblePerson.EmailAddress;
            //    //updateFacility.ResposiblePerson.ModifiedUserId = resposiblePerson.ModifiedUserId;
            //    //updateFacility.ResposiblePerson.ModifiedDate = DateTime.Now;
            //    unit.Facilities.Update(updateFacility);
            //    unit.SaveChanges();
            //}
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
