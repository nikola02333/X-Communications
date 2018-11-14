using System;
using System.Collections.Generic;

namespace XCommunications.Models
{
    public partial class Worker
    {
        public Worker()
        {
            Contract = new HashSet<Contract>();
            RegistratedUser = new HashSet<RegistratedUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Operater { get; set; }

        public ICollection<Contract> Contract { get; set; }
        public ICollection<RegistratedUser> RegistratedUser { get; set; }
    }
}
