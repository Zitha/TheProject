using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using TheProject.ReportWebApplication.Models;

namespace TheProject.ReportWebApplication.Services
{
    public class FacilityService
    {
        #region Properties
        readonly string baseUri = WebConfigurationManager.AppSettings["APIurl"] + "Facility";
        #endregion

        #region Methods
        public List<Facility> GetFacilities()
        {
            try
            {
                string uri = baseUri + "GetFacilities";
                using (HttpClient httpClient = new HttpClient())
                {
                    Task<String> response = httpClient.GetStringAsync(uri);
                    var result = JsonConvert.DeserializeObject<List<Facility>>(response.Result);
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Facility> GetFacilityByClientCode(string clientCode)
        {
            try
            {
                //string uri = baseUri + "GetFacilities";
                string uri = baseUri + "/Login?username=" + clientCode;
                using (HttpClient httpClient = new HttpClient())
                {
                    Task<String> response = httpClient.GetStringAsync(uri);
                    var result = JsonConvert.DeserializeObject<List<Facility>>(response.Result);
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion
    }
}