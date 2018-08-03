using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheProject.ReportWebApplication.Models
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClientCode { get; set; }
        public string SettlementType { get; set; }
        public string Zoning { get; set; }
        public string IDPicture { get; set; }
        public string Status { get; set; }
        public string Region { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ModifiedUserId { get; set; }
    }
    
}