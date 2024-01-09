using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.Model
{
    public class ConditionAssessment
    {
        public int Id { get; set; }       
        public string Roof { get; set; }
        public string Walls { get; set; }
        public string DoorsWindows { get; set; }
        public string Floors { get; set; }
        public string Civils { get; set; }
        public string Plumbing { get; set; }
        public string Electrical { get; set; }
    }
}
