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
                        userId = resposiblePerson.CreatedUserId;
                        Create(ref unit, resposiblePerson);
                    }
                    else
                    {
                        userId = resposiblePerson.ModifiedUserId.Value;
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
            resposiblePerson.CreatedDate = DateTime.Now;
            unit.People.Add(resposiblePerson);
            unit.SaveChanges();
        }

        private void Update(ref ApplicationUnit unit, Person resposiblePerson)
        {
            resposiblePerson.ModifiedDate = DateTime.Now;
            unit.People.Update(resposiblePerson);
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
