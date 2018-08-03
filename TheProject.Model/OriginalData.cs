using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.Model
{
    public class OriginalData
    {
        [Key]
        public double OBJECTID { get; set; }
        public string TrackerDoNotDo { get; set; }
        public string REGION { get; set; }
        public string GIS_KEY { get; set; }
        public string VENUS_CODE { get; set; }
        public string TOWNSHIP { get; set; }
        public string PROPDESC { get; set; }
        public string WARD_NO { get; set; }
        public double? LATITUDE { get; set; }
	    public double? LONGITUDE { get; set; }
	    public string Stand_Descrip { get; set; }
	    public string Owner_Name { get; set; }
	    public string Prop_Category { get; set; }
	    public string Physical_Addr { get; set; }
	    public string Street_No { get; set; }
	    public double? Size_ { get; set; }
	    public double? Market_Value { get; set; }
	    public string Usage_Descrip { get; set; }
	    public string Empty_Stand_Ind { get; set; }
	    public double? Deed_Date { get; set; }
	    public string Deed_No { get; set; }
	    public double? Register_Date { get; set; }
	    public string Register_No { get; set; }
	    public double? Capture_Date { get; set; }
	    public string ZONING { get; set; }
	    public string AREA_CODE { get; set; }
        public double? One { get; set; }
    }
}
