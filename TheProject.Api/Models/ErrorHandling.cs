using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheProject.Data;
using TheProject.Model;

namespace TheProject.Api.Models
{
    public class ErrorHandling
    {
        public static void LogError(string errorMessase, string source)
        {
            using (ApplicationUnit unit = new ApplicationUnit())
            {
                ErrorLog errorLog = new ErrorLog
                {
                    Date = DateTime.Now,
                    ErrorMessage = errorMessase,
                    Source = source
                };
                unit.ErrorLogs.Add(errorLog);
                unit.SaveChanges();
            }
        }
    }
}