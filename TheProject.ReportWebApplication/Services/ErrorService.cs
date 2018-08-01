using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using TheProject.Data;
using TheProject.ReportWebApplication.Models;

namespace TheProject.ReportWebApplication.Services
{
    public class ErrorService
    {
        #region Properties
        private readonly string baseUri = WebConfigurationManager.AppSettings["APIurl"] + "ErrorLog";
        #endregion

        #region Methods

        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="errorLog"></param>
        /// <returns></returns>
        public async Task Log(ErrorLog errorLog)
        {
            ApplicationUnit unit = new ApplicationUnit();

            Model.ErrorLog errorLogd = new Model.ErrorLog();
            errorLogd.ErrorMessage = errorLog.ErrorMessage;
            errorLogd.Source = errorLog.Source;
            errorLogd.Date = errorLog.Date;
            unit.ErrorLogs.Add(errorLogd);
            unit.SaveChanges();
        }
        #endregion
    }
}