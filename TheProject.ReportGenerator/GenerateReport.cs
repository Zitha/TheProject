using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheProject.Data;
using TheProject.Model;
using System.IO;
using System.Net.Http;
using System.Data.Entity;

namespace TheProject.ReportGenerator
{
    public class GenerateReport
    {

        public Dictionary<string, string> GenerateAllReport()
        {
            //List<string> list = new List<string>();

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            FacilityReport facilityReport = new FacilityReport();
            ApplicationUnit unit = new ApplicationUnit();

            List<Facility> facilities = unit.Facilities.GetAll()
                                        .Include(b => b.Buildings)
                                        .Include(d => d.DeedsInfo)
                                        .Include(c => c.Portfolio)
                                        .Include(p => p.ResposiblePerson)
                                        .Include(l => l.Location)
                                        .ToList();

            List<Facility> facilitiffes = facilities.Where(f => f.Status == "Submitted").ToList();

            int i = 0;
            foreach (var facility in facilitiffes)
            {
                if (i < 30)
                {
                    Model.OriginalData dbOriginalData = unit.OriginalDatas.GetAll().Where(o => o.VENUS_CODE.Trim().ToLower() == facility.ClientCode.Trim().ToLower()).FirstOrDefault();
                    string facilityLocation = facilityReport.GenerateFacilityReport(facility, dbOriginalData);
                    dictionary.Add(facilityLocation, facility.ClientCode);
                    i++;
                }
            }

            return dictionary;
        }

        public string GenerateOneReport(string clientCode)
        {
            FacilityReport facilityReport = new FacilityReport();
            ApplicationUnit unit = new ApplicationUnit();

            List<Facility> facilities = unit.Facilities.GetAll()
                                        .Include(b => b.Buildings)
                                        .Include(d => d.DeedsInfo)
                                        .Include(c => c.Portfolio)
                                        .Include(p => p.ResposiblePerson)
                                        .Include(l => l.Location)
                                        .ToList();

            Facility facility = facilities.Where(f => f.ClientCode.Trim().ToLower() == clientCode.Trim().ToLower()).FirstOrDefault();
            Model.OriginalData dbOriginalData = unit.OriginalDatas.GetAll().Where(o => o.VENUS_CODE.Trim().ToLower() == facility.ClientCode.Trim().ToLower()).FirstOrDefault();
            int i = 0;
            if (facility != null)
            {
                string facilityLocation = facilityReport.GenerateFacilityReport(facility, dbOriginalData);
                return facilityLocation;
            }
            else {
                return null;
            }
        }

    }   
}
