using System;
using System.Collections.Generic;

namespace XCommunications.Models
{
    public partial class Contract
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int WorkerId { get; set; }
        public string Tarif { get; set; }

        public Customer Customer { get; set; }
        public Worker Worker { get; set; }
    }
}
