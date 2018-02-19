using System;

namespace TheProject.Model
{
    public class Client
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public string ClientId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? ModifiedUserId { get; set; }
    }
}