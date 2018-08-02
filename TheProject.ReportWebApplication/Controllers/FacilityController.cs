﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TheProject.Data;
using TheProject.ReportGenerator;
using TheProject.ReportWebApplication.Models;
using TheProject.ReportWebApplication.Services;
using System.Data.Entity;
using System.Net;
using System.Net.Mime;
using System.IO;
using System.IO.Compression;

namespace TheProject.ReportWebApplication.Controllers
{
    public class FacilityController : BaseController
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
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ClientCode")] Facility facility)
        {
            FacilityReport facilityReport = new FacilityReport();
            ApplicationUnit unit = new ApplicationUnit();
            Model.Facility dbFacility = unit.Facilities.GetAll()
                                        .Include(b => b.Buildings)
                                        .Include(d => d.DeedsInfo)
                                        .Include(c => c.Portfolio)
                                        .Include(p => p.ResposiblePerson)
                                        .Include("Location.GPSCoordinates")
                                        .Include("Location.BoundryPolygon")
                                        .Where(f => f.ClientCode == facility.ClientCode).FirstOrDefault();

            if (dbFacility == null)
            {
                ModelState.AddModelError("", "Facility Does Not Exist.");
                return View(facility);
            }
            var filePath = facilityReport.GenerateFacilityReport(dbFacility);

            using (var webClient = new WebClient())
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return null;
                }
                byte[] file = webClient.DownloadData(filePath);
                return File(file, MediaTypeNames.Application.Pdf);
            }
        }

        // GET: Facility/Edit/5
        [HttpPost]
        public ActionResult DownloadAllFacility()
        {
            FacilityReport facilityReport = new FacilityReport();
            ApplicationUnit unit = new ApplicationUnit();

            List<Model.Facility> dbFacilities = unit.Facilities.GetAll()
                                        .Include(b => b.Buildings)
                                        .Include(d => d.DeedsInfo)
                                        .Include(c => c.Portfolio)
                                        .Include(p => p.ResposiblePerson)
                                        .Include("Location.GPSCoordinates")
                                        .Include("Location.BoundryPolygon")
                                        .Where(ss => ss.Status == "Submitted")
                                        .ToList();
            using (var memoryStream = new MemoryStream())
            {
                using (ZipArchive ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var facility in dbFacilities)
                    {
                        var filePath = facilityReport.GenerateFacilityReport(facility);
                        ziparchive.CreateEntryFromFile(filePath, facility.ClientCode + ".pdf");
                    }
                }
                return File(memoryStream.ToArray(), "application/zip", "facilities.zip");
            }
        }
        #endregion
    }
}
