using System;
using System.Collections.Generic;

namespace XCommunications.Models
{
    public partial class RegistratedUser
    {
        public int Id { get; set; }
        public int Imsi { get; set; }
        public int CustomerId { get; set; }
        public int WorkerId { get; set; }
        public int NumberId { get; set; }

        public Customer Customer { get; set; }
        public Simcard ImsiNavigation { get; set; }
        public Number Number { get; set; }
        public Worker Worker { get; set; }
    }
}
