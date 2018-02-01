using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
            return new List<Facility>();
        }
    }
}
