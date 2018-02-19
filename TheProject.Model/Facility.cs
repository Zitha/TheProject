using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.Model
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

        public virtual  DeedsInfo DeedsInfo { get; set; }

        public virtual  Person ResposiblePerson { get; set; }

        public virtual Location Location { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? ModifiedUserId { get; set; }

        public virtual Portfolio Portfolio { get; set; }

        public virtual  List<Building> Buildings { get; set; }

    }
}
