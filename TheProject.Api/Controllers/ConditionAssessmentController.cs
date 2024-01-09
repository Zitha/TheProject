using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
    public class ConditionAssessmentController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AddConditionAssessment(ConditionAssessment conditionAssessment)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {

                    unit.ConditionAssessments.Add(conditionAssessment);
                    unit.SaveChanges();

                    Task addTask = new Task(() => LogAuditTrail("Condition Assessment", "Add", conditionAssessment.Id, conditionAssessment.Id));
                    addTask.Start();

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        content = conditionAssessment
                    });
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace, "AddConditionAssessment");
                return Request.CreateResponse(ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateConditionAssessment(ConditionAssessment conditionAssessment)
        {
            ApplicationUnit unit = new ApplicationUnit();
            try
            {
                ConditionAssessment updateConditionAssessment = unit.ConditionAssessments.GetAll().FirstOrDefault(c => c.Id == conditionAssessment.Id);
                if (updateConditionAssessment != null)
                {
                    updateConditionAssessment.Id = conditionAssessment.Id;
                    //updateConditionAssessment.BuildingId = conditionAssessment.BuildingId;
                    updateConditionAssessment.Roof = conditionAssessment.Roof.Trim();
                    updateConditionAssessment.Walls = conditionAssessment.Walls.Trim();
                    updateConditionAssessment.DoorsWindows = conditionAssessment.DoorsWindows.Trim();
                    updateConditionAssessment.Floors = conditionAssessment.Floors.Trim();
                    updateConditionAssessment.Civils = conditionAssessment.Civils.Trim();
                    updateConditionAssessment.Plumbing = conditionAssessment.Plumbing.Trim();
                    updateConditionAssessment.Electrical = conditionAssessment.Electrical.Trim();

                    unit.ConditionAssessments.Update(updateConditionAssessment);
                    unit.SaveChanges();

                    Task updateTask = new Task(() => LogAuditTrail("ConditionAssessment", "Update", updateConditionAssessment.Id, updateConditionAssessment.Id));
                    updateTask.Start();

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        content = updateConditionAssessment
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                unit.Dispose();
                ErrorHandling.LogError(ex.StackTrace, "UpdateBuilding");
                return Request.CreateResponse(ex);
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