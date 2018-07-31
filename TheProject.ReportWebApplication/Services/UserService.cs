using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using TheProject.ReportWebApplication.Models;

namespace TheProject.ReportWebApplication.Services
{
    public class UserService
    {
        #region Properties
        readonly string baseUri = WebConfigurationManager.AppSettings["APIurl"] + "User";
        #endregion

        #region Methods
        public User Login(string username, string password)
        {
            try
            {
                string uri = baseUri + "/Login?username=" + username + "&password=" + password;
                using (HttpClient httpClient = new HttpClient())
                {
                    Task<String> response = httpClient.GetStringAsync(uri);
                    var result = JsonConvert.DeserializeObject<User>(response.Result);

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