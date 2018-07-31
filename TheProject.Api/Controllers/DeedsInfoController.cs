using System;
using System.Collections.Generic;
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
    public class DeedsInfoController : ApiController
    {
        // GET: DeedsInfo/Details/5
        [HttpGet]
        public DeedsInfo GetDeedsInfoById(int id)
        {
            try
            {
                ApplicationUnit unit = new ApplicationUnit();
                return unit.DeedsInfos.GetById(id);
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace, "GetDeedsInfo");
                throw ex;
            }
        }

        // POST: DeedsInfo/CreateEdit
        [HttpPost]
        public HttpResponseMessage CreateEdit(DeedsInfo deedsInfo)
        {
            ApplicationUnit unit = new ApplicationUnit();
            string action = deedsInfo.Id != 0 ? "Update" : "Create";
            int userId = 0;
            try
            {
                if (deedsInfo != null)
                {
                    if (deedsInfo.Id == 0)
                    {
                       // userId = deedsInfo.CreatedUserId;
                        Create(ref unit, deedsInfo);
                    }
                    else
                    {
                       // userId = deedsInfo.ModifiedUserId.Value;
                        Update(ref unit, deedsInfo);
                    }
                }

                unit.Dispose();
                Task updateTask = new Task(() => LogAuditTrail("DeedsInfo", action, userId, deedsInfo.Id));
                updateTask.Start();
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    content = true
                });
            }
            catch (Exception ex)
            {
                unit.Dispose();
                ErrorHandling.LogError(ex.StackTrace, action + "DeedsInfo");
                return Request.CreateResponse(ex);
            }
        }

        private void Create(ref ApplicationUnit unit, DeedsInfo deedsInfo)
        {
            ////deedsInfo.CreatedDate = DateTime.Now;
            //Facility updateFacility = unit.Facilities.GetAll().FirstOrDefault(fc => fc.Id == deedsInfo.FacilityId);
            //if (updateFacility != null)
            //{
            //    updateFacility.DeedsInfo = deedsInfo;
            //    unit.Facilities.Update(updateFacility);
            //    unit.SaveChanges();
            //}
        }

        private void Update(ref ApplicationUnit unit, DeedsInfo deedsInfo)
        {
            //Facility updateFacility = unit.Facilities.GetAll().FirstOrDefault(fc => fc.Id == deedsInfo.FacilityId);
            //if (updateFacility != null)
            //{
            //    updateFacility.DeedsInfo.ErFNumber = deedsInfo.ErFNumber;
            //    updateFacility.DeedsInfo.TitleDeedNumber = deedsInfo.TitleDeedNumber;
            //    updateFacility.DeedsInfo.Extent = deedsInfo.Extent;
            //    updateFacility.DeedsInfo.OwnerInfomation = deedsInfo.OwnerInfomation;
            //    updateFacility.DeedsInfo.ModifiedUserId = deedsInfo.ModifiedUserId;
            //    updateFacility.DeedsInfo.ModifiedDate = DateTime.Now;
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
