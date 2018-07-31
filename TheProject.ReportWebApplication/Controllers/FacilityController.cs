using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheProject.ReportWebApplication.Models;
using TheProject.ReportWebApplication.Services;
using TheProject.ReportGenerator;
using System.IO.Compression;

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
        public ActionResult DownloadFacility([Bind(Include = "ClientCode")] Facility facility)
        {
            if (string.IsNullOrEmpty(facility.ClientCode))
            {
                ModelState.AddModelError("", "Please Enter Client Code.");
                return RedirectToAction("Index", "Facility");
            }
            else {
                GenerateReport generateReport = new GenerateReport();
                string fullPath = generateReport.GenerateOneReport(facility.ClientCode);
                if (string.IsNullOrEmpty(fullPath))
                {
                    ModelState.AddModelError("", "Please Enter Client Code.");
                    return RedirectToAction("Index", "Facility");
                }
                else {
                    byte[] fileBytes = GetFile(fullPath);
                    return File(fileBytes, "application/pdf", facility.ClientCode + ".pdf");
                }               
            }
           
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        // GET: Facility/Edit/5
        [HttpPost]
        public FileResult DownloadAllFacility()
        {
            GenerateReport generateReport = new GenerateReport();
            Dictionary<string, string> fullPathList = generateReport.GenerateAllReport();
            using (var memoryStream = new MemoryStream())
            {
                using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var item in fullPathList)
                    {
                        ziparchive.CreateEntryFromFile(item.Key, item.Value + ".pdf");
                    }
                }
                return File(memoryStream.ToArray(), "application/zip", "facilities.zip");
            }
        }
        #endregion
    }
}
