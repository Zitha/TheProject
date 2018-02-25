using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.Model
{
    public class Audit
    {
        public int Id { get; set; }

        public string Section { get; set; }
        public string Type { get; set; }

        public int UserId { get; set; }

        public DateTime ChangeDate { get; set; }

        public int ItemId { get; set; }
    }
}
