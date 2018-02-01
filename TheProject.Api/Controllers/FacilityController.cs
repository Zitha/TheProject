using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheProject.Data;
using TheProject.Model;

namespace TheProject.Api.Controllers
{
    public class FacilityController : ApiController
    {
        public Facility UpdateFacility(Facility facility)
        {
            return new Facility();
        }

        public Facility AddFacility(Facility facility)
        {
            return new Facility();
        }

        public IEnumerable<Facility> GetFacilities()
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    List<Facility> facilities = unit.Facilities.GetAll().ToList();

                    return facilities;
                }
            }
            catch (Exception ex)
            {
                var outputLines = new List<string>();
                outputLines.Add(ex.Message);
                File.AppendAllLines(@"c:\errors.txt", outputLines);
                throw;
            }
        }
    }
}
