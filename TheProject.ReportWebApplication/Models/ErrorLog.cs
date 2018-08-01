using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.ReportWebApplication.Models
{
    public class ErrorLog
    {
        public long ErrorLogID { get; set; }
        public string ErrorMessage { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
    }
}
