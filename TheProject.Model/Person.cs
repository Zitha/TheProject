using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheProject.Model
{
    public class Person
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Designation { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? ModifiedUserId { get; set; }

        [NotMapped]
        public int? FacilityId { get; set; }
    }
}