using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.Model
{
    public class ErrorLog
    {
        public int Id { get; set; }

        public string ErrorMessage { get; set; }

        public string Source { get; set; }

        public DateTime Date { get; set; }
    }
}
