using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.Models
{
    public partial class RegistratedUser
    {
        public int Id { get; set; }
        public int Imsi { get; set; }
        public int IdentificationCard { get; set; }
        public int Worker { get; set; }

        public Customer IdentificationCardNavigation { get; set; }
        public Simcard ImsiNavigation { get; set; }
        public Worker WorkerNavigation { get; set; }
    }
}
