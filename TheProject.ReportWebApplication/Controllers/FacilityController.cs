using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheProject.ReportWebApplication.Models;
using TheProject.ReportWebApplication.Services;

namespace TheProject.ReportWebApplication.Controllers
{
    public class FacilityController : Controller
    {
        #region Properties
        private FacilityService facilityService;

        #endregion

        #region Constructor
        public FacilityController()
        {
            this.facilityService = new FacilityService();
        }
        #endregion

        #region Methods

        // GET: Facility
        public ActionResult Index()
        {
            return View();
        }

        // GET: Facility/Edit/5
        [HttpPost]
        public ActionResult DownloadFacility([Bind(Include = "ClientCode")] Facility facility)
        {
            return View();
        }

        // GET: Facility/Edit/5
        [HttpPost]
        public ActionResult DownloadAllFacility()
        {
            return View();
        }
        #endregion
    }
}
