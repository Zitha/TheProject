using System.Collections.Generic;
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
using System.Configuration;
using TheProject.ReportWebApplication.Utilities;

namespace TheProject.ReportWebApplication.Controllers
{
    public class FacilityController : BaseController
    {
        #region Properties
        private FacilityService facilityService;
        int _defaultPageSize = 20;

        #endregion

        #region Constructor
        public FacilityController()
        {
            this.facilityService = new FacilityService();
        }
        #endregion

        #region Methods

        // GET: Facility
        public ActionResult Index(int? page)
        {
            List<Facility> facilities = GetSubmittedFacilities();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IPagedList<Facility> providersListPaged = facilities.ToPagedList(currentPageIndex, _defaultPageSize);

            List<string> regions = facilities.Select(d => d.Region).Distinct().ToList();

            ViewBag.Regions = new SelectList(regions);

            return View(providersListPaged);
        }

        private List<Facility> GetSubmittedFacilities()
        {
            using (ApplicationUnit unit = new ApplicationUnit())
            {
                var dbfacilities = unit.Facilities.GetAll()
                                      .Include(b => b.Buildings)
                                      .Include(d => d.DeedsInfo)
                                      .Include(p => p.ResposiblePerson)
                                      .Include("Location.GPSCoordinates")
                                      .Include("Location.BoundryPolygon")
                                      .Where(ss => ss.Status == "Submitted")
                                      .ToList();
                List<Facility> facilities = new List<Facility>();
                foreach (var item in dbfacilities)
                {
                    facilities.Add(new Facility
                    {
                        ClientCode = item.ClientCode,
                        SettlementType = item.SettlementType,
                        Zoning = item.Zoning,
                        Region = item.Location.Region
                    });
                }
                return facilities;
            }
        }

        // GET: Facility/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DownloadFacility(string clientCode)
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
                                        .Where(f => f.ClientCode.ToLower() == clientCode.ToLower()).FirstOrDefault();
            if (dbFacility == null)
            {
                return RedirectToAction("Index");
            }

            Model.OriginalData dbOriginalData = unit.OriginalDatas.GetAll()
                .Where(o => o.VENUS_CODE.Trim().ToLower()
                == dbFacility.ClientCode.Trim().ToLower())
                .FirstOrDefault();

            string filePath = facilityReport.GenerateFacilityReport(dbFacility, dbOriginalData);

            using (var webClient = new WebClient())
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return null;
                }
                byte[] file = webClient.DownloadData(filePath);
                DeleteAllFile();
                return File(file, MediaTypeNames.Application.Pdf);
            }
        }

        // GET: Facility/Edit/5
        [HttpPost]
        public ActionResult DownloadAllFacility([Bind(Include = "Region")] Facility facility)
        {

            if (string.IsNullOrEmpty(facility.Region))
            {
                ModelState.AddModelError("", "Please Select Region.");
                return View(facility);

            }
            FacilityReport facilityReport = new FacilityReport();
            ApplicationUnit unit = new ApplicationUnit();

            List<Model.Facility> dbFacilities = unit.Facilities.GetAll()
                                        .Include(b => b.Buildings)
                                        .Include(d => d.DeedsInfo)
                                        .Include(p => p.ResposiblePerson)
                                        .Include("Location.GPSCoordinates")
                                        .Include("Location.BoundryPolygon")
                                        .Where(ss => ss.Status == "Submitted" && ss.Location.Region.Trim().ToLower() == facility.Region.Trim().ToLower())
                                        .ToList();

            if (dbFacilities.Count() == 0)
            {
                ModelState.AddModelError("", "No Facilities Found For Selected Region.");
                return null;
            }
            else
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (ZipArchive ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var item in dbFacilities)
                        {
                            Model.OriginalData dbOriginalData = unit.OriginalDatas.GetAll().Where(o => o.VENUS_CODE.Trim().ToLower() == item.ClientCode.Trim().ToLower()).FirstOrDefault();
                            string filePath = facilityReport.GenerateFacilityReport(item, dbOriginalData);
                            ziparchive.CreateEntryFromFile(filePath, item.ClientCode + ".pdf");
                        }
                    }
                    DeleteAllFile();
                    return File(memoryStream.ToArray(), "application/zip", "facilities.zip");
                }
            }
        }

        private void DeleteAllFile()
        {

            string _currpath = ConfigurationManager.AppSettings["ReportsPath"];
            DirectoryInfo di = new DirectoryInfo(_currpath);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

        }

        public ViewResultBase Search(string search)
        {
            const int currentPageIndex = 0;
            List<Facility> facilities = GetSubmittedFacilities();

            if (!string.IsNullOrEmpty(search))
            {
                facilities = facilities.OrderBy(s => s.ClientCode)
                    .Where(
                        s =>
                            s.ClientCode.ToUpper().Contains(search.ToUpper())).ToList();
            }
            IPagedList<Facility> providersListPaged = facilities.ToPagedList(currentPageIndex,
               _defaultPageSize);

            List<string> regions = facilities.Select(d => d.Region).Distinct().ToList();
            ViewBag.Regions = new SelectList(regions);

            if (Request.IsAjaxRequest())
                return PartialView("Index", providersListPaged);
            return View("Index", providersListPaged);
        }
        #endregion
    }
}
