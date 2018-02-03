using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.Model
{
    public class Portfolio
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Client Client { get; set; }

        public List<Facility> Facilities { get; set; }

    }
}
