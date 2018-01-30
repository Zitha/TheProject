using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public List<Facility> Facilities { get; set; }
    }
}
